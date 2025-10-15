using GymManagementDAL.Entities;
using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.TrainerViewModels
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public Speciality? Speciality { get; set; } 

        public string? BirthDay { get; set; }

        public string? Address { get; set; }


    }
}
