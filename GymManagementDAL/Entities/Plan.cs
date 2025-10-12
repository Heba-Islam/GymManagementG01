using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Plan : BaseEntity
    {

        public string Name { get; set; } = null!;

        public int DurationDays { get; set; }

        public decimal Price { get; set; }


        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        #region relationships
        #region has many memberships

        public ICollection<Membership> Memberships { get; set; } = null!;


        #endregion
        #endregion
    }
}
