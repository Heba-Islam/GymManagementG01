using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Trainer:GymUser
    {
        public Speciality Speciality { get; set; }

        //HireDate == CreatedAt

        #region Realtionships
        #region has many sessions
        [Required]
        public ICollection<Session> Sessions { get; set; }

        #endregion
        #endregion

    }
}
