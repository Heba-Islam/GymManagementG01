using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Plan : BaseEntity
    {
        [Required]

        public string Name { get; set; }

        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        [Required]

        public string Description { get; set; }

        public bool IsActive { get; set; }

        #region relationships
        #region has many memberships
        [Required]

        public ICollection<Membership> Memberships { get; set; }


        #endregion
        #endregion
    }
}
