using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.Entities;
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
        private readonly IGenericRepository<Member> _memberRepoistory;

        public MemberService(IGenericRepository<Member> memberRepoistory)
        {
            _memberRepoistory = memberRepoistory;
        }

        //all functions deal with modellllls not repos
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepoistory.GetAll();

            if(members == null || !members.Any())
            {
                return [];

            }

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
    }
}
