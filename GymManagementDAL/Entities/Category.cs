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
    public class Category:BaseEntity
    {

        public string CategoryName { get; set; } = null!;

        #region relationships
        #region has many sessions
        public ICollection<Session> Sessions { get; set; } = null!;

        #endregion
        #endregion

    }
}
