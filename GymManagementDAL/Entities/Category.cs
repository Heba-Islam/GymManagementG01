using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Entities
{
    internal class Category:BaseEntity
    {
        [Required]

        public string CategoryName { get; set; }

        #region relationships
        #region has many sessions
        [Required]

        public ICollection<Session> Sessions { get; set; }

        #endregion
        #endregion

    }
}
