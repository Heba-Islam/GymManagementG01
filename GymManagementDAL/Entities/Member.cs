using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Member:GymUser
    {
        [Required]

        public string Photo { get; set; }
        //joinDate == CreatedAt

        #region Relationships

        #region has health record
        [Required]

        public HealthRecord HealthRecord { get; set; }

        #endregion

        #region member has memberships
        [Required]

        public ICollection<Membership> Memberships { get; set; }


        #endregion

        #region member has many sessions
        [Required]

        public ICollection<MemberSessions> MemberSessions { get; set; }


        #endregion

        #endregion
    }
}
