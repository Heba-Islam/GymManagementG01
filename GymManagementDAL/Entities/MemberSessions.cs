using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class MemberSessions : BaseEntity
    {
        public int MemberId { get; set; }
        [Required]

        public Member Member { get; set; }

        public int SessionId { get; set; }
        [Required]

        public Session Session { get; set; }

        public bool IsAttended { get; set; }


    }
}
