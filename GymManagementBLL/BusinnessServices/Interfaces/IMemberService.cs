using GymManagementBLL.View_Models;
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


    }
}
