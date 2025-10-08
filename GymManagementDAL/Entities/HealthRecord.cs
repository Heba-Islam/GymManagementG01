using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class HealthRecord:BaseEntity
    {
        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public string BloodType { get; set; }

        public string? Notes { get; set; }

        //LastUpdate==UpdatedAt

    }
}
