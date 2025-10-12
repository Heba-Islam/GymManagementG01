using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Trainer:GymUser
    {
        public Speciality Speciality { get; set; }

        //HireDate == CreatedAt

        #region Realtionships
        #region has many sessions
        public ICollection<Session> Sessions { get; set; } = null!;

        #endregion
        #endregion

    }
}
