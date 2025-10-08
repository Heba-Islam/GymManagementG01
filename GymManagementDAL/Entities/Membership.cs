using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Membership:BaseEntity
    {
        public int MemberId { get; set; }
        [Required]

        public Member Member { get; set; }
        [Required]

        public Plan Plan { get; set; }
        public int PlanId { get; set; }

        public DateTime EndDate { get; set; }

        [Required]

        public string Status
        {
            get
            {
                if (EndDate < DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
            set;
        }

    }
}
