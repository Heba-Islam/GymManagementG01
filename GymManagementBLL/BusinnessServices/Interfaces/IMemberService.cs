using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Interfaces
{
    internal interface IMemberService
    {
        public IEnumerable<MemberViewModel> GetAllMembers();

       public bool CreateMember(CreateAMemberViewModel memberViewModel);

        public HealthRecordViewModel? GetMemberHealthRecord(int memberId);

        public MemberViewModel? GetMemberDetails(int memberId);

        public MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberId);

        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate);

        bool RemoveMember(int MemberId);


    }
}
