using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models
{
    public class MemberViewModel
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;

        public string? Photo { get; set; }

        public string Email { get; set; } = null!;

        public string Gender { get; set; } = null!;



    }
}
