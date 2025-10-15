using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Member:GymUser
    {

        public string? Photo { get; set; }
        //joinDate == CreatedAt

        #region Relationships

        #region has health record

        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion

        #region member has memberships

        public ICollection<Membership> Memberships { get; set; } = null!;


        #endregion

        #region member has many sessions

        public ICollection<MemberSessions> MemberSessions { get; set; } = null!;

        



        #endregion

        #endregion
    }
}
