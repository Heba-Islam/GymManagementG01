using AutoMapper;
using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region create a new member
        public bool CreateMember(CreateAMemberViewModel createMember)
        {

            if (DoesEmailExist(createMember.Email) || DoesPhoneExist(createMember.Phone))
                return false;


            //CreateMemberViewModel=>Member

            #region Manual Mapping
            //var member = new Member
            //{
            //    Name = createMember.Name,
            //    Email = createMember.Email,
            //    Phone = createMember.Phone,
            //    Gender = createMember.Gender,
            //    DateOfBirth = createMember.DateOfBirth,
            //    Address = new Address
            //    {
            //        BuildingNumber = createMember.BuildingNumber,
            //        City = createMember.City,
            //        Street = createMember.Street,
            //    },
            //    HealthRecord = new HealthRecord
            //    {
            //        Height = createMember.HealthRecord.Hieght,
            //        Weight = createMember.HealthRecord.Wieght,
            //        BloodType = createMember.HealthRecord.BloodType,
            //        Note = createMember.HealthRecord.Note,
            //    }
            //}; 
            #endregion

            //Create Member in Database

            var member = _mapper.Map<CreateAMemberViewModel, Member>(createMember);

            _unitOfWork.GetRepository<Member>().Add(member);

            return _unitOfWork.SaveChanges() > 0;

        }

        #endregion

        #region get all members
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();

            if (members is null || !members.Any()) return [];

            return _mapper.Map<IEnumerable<MemberViewModel>>(members);

        }

        #endregion

        #region get a member's details
        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;

            var memberViewModel = _mapper.Map<MemberViewModel>(member);

            var memberShip = _unitOfWork.GetRepository<Membership>().GetAll(X => X.MemberId == memberId && X.Status == "Active")
                      .FirstOrDefault();

            if (memberShip is not null)
            {
                memberViewModel.MembershipStartDate = memberShip.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = memberShip.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRepository<Plan>().GetById(memberShip.PlanId);

                memberViewModel.PlanName = plan?.Name;
            }

            return memberViewModel;
        }

        #endregion

        #region get member's details for update and update a member
        public MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberId)
        {

            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null) return null;

            return _mapper.Map<MemberToUpdateViewModel>(member);

        }
        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                var memberRepository = _unitOfWork.GetRepository<Member>();

                if (DoesEmailExist(memberToUpdate.Email) || DoesPhoneExist(memberToUpdate.Phone))
                    return false;

                var member = memberRepository.GetById(memberId);

                if (member is null) return false;

                _mapper.Map(memberToUpdate, member);

                memberRepository.Update(member);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region get health record
        public HealthRecordViewModel? GetMemberHealthDetails(int memberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);

            if (memberHealthRecord is null) return null;

            return _mapper.Map<HealthRecordViewModel>(memberHealthRecord);

        }

        #endregion

        #region delete a member
        public bool RemoveMember(int memberId)
        {
            try
            {
                var memberRepository = _unitOfWork.GetRepository<Member>();
                var member = memberRepository.GetById(memberId);

                if (member is null) return false;

                var memberSessionsIds = _unitOfWork.GetRepository<MemberSessions>()
                    .GetAll(X => X.MemberId == memberId)
                    .Select(X => X.SessionId);

                var hasFutureSessions = _unitOfWork.GetRepository<Session>().GetAll(
                   S => memberSessionsIds.Contains(S.Id) && S.StartTime > DateTime.Now).Any();
                if (hasFutureSessions)
                    return false;


                var membershipRepository = _unitOfWork.GetRepository<Membership>();
                var memberShips = membershipRepository.GetAll(X => X.MemberId == memberId);

                if (memberShips.Any())
                {
                    foreach (var membership in memberShips)
                    {
                        membershipRepository.Delete(membership);  //Transaction
                    }
                }

                memberRepository.Delete(member); //Transaction

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #endregion

        #region helper methods
        private bool DoesEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Email == email).Any();
        }

        private bool DoesPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == phone).Any();
        } 
        #endregion
    }
}