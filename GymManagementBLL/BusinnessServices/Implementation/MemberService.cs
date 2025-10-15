using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.implementation;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Implementation
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        //all functions deal with repos not models
        #region Get All Members
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any()) return [];

            #region manual mapping
            //var ListOfMemberViewModels = new List<MemberViewModel>();

            //foreach(var member in members)
            //{
            //    var memberViewModel = new MemberViewModel
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Photo = member.Photo,
            //        Email = member.Email,
            //        Phone = member.Phone,
            //        Gender = member.Gender.ToString(),
            //    };
            //    ListOfMemberViewModels.Add(memberViewModel);


            //}

            //return ListOfMemberViewModels;
            #endregion
            //linq mapping
            var ListOfMemberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Photo = m.Photo,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString(),
            });

            return ListOfMemberViewModels;

        }
        #endregion

        #region create a member

        public bool CreateMember(CreateAMemberViewModel createAMember)
        {
            try
            {
                if (doesEmailExist(createAMember.Email) || doesPhoneExist(createAMember.Phone)) return false;

                var member = new Member
                {
                    Name = createAMember.Name,
                    Email = createAMember.Email,
                    Phone = createAMember.Phone,
                    Gender = createAMember.Gender,
                    BirthDay = createAMember.DateOfBirth,
                    Address = new Address
                    {
                        BuildingNumber = createAMember.BuildingNumber,
                        Street = createAMember.Street,
                        City = createAMember.City,
                    },
                    HealthRecord = new HealthRecord
                    {
                        BloodType = createAMember.HealthRecord.BloodType,
                        Height = createAMember.HealthRecord.Height,
                        Weight = createAMember.HealthRecord.Weight,
                        Notes = createAMember.HealthRecord.Note,
                    },

                };

                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;

            }

        }
        #endregion

        #region get health record
        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
            if (memberHealthRecord is null)
            {
                return null;
            }
            var healthRecordViewModel = new HealthRecordViewModel
            {
                BloodType = memberHealthRecord.BloodType,
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                Note = memberHealthRecord.Notes,
            };

            return healthRecordViewModel;

        }


        #endregion

        #region Get Member Details

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;

            var memberViewModel = new MemberViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                Birthday = member.BirthDay.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
                Photo = member.Photo,
            };

            var membership = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == memberId && x.Status == "Active")
                        .FirstOrDefault();
            if (membership != null)
            {
                memberViewModel.MembershipStartDate = membership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipStartDate = membership.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRepository<Plan>().GetById(membership.PlanId);
                if (plan != null)
                {
                    memberViewModel.PlanName = plan.Name;
                }

            }
            return memberViewModel;


        }

        #endregion


        #region Get member details to update 
        public MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = member.Email,
                Name = member.Name,
                Phone = member.Phone,
                Photo = member.Photo,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.City
            };

        }
        #endregion

        #region update member 
        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                if (doesEmailExist(memberToUpdate.Email) || doesPhoneExist(memberToUpdate.Phone)) return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null) return false;

                member.Phone = memberToUpdate.Phone;
                member.Email = memberToUpdate.Email;
                member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Address.Street = memberToUpdate.Street;
                member.Address.City = memberToUpdate.City;
                member.UpdatedAt = DateTime.Now;

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion


        #region remove member
        public bool RemoveMember(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return false;

            var HasActiveSession = _unitOfWork.GetRepository<MemberSessions>()
                .GetAll(x => x.MemberId == memberId && x.Session.StartTime > DateTime.Now).Any();

            if (HasActiveSession) return false;

            var membership = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == memberId);
            try
            {
                if (membership.Any())
                {
                    foreach (var m in membership)
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(m);
                    }
                }

                _unitOfWork.GetRepository<Member>().Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion


        #region helper methods

        private bool doesEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }

        private bool doesPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }


        #endregion




    }
}
