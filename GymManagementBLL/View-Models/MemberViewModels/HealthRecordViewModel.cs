using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage ="height is mandatory")]
        [Range(1,300,ErrorMessage = "height must be between 1 and 300 cm")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "weight is mandatory")]
        [Range(1, 350, ErrorMessage = "weight must be between 1 and 350 kg")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "blood type is madatory")]
        [StringLength(3,MinimumLength = 2 , ErrorMessage = "blood type must be between 2 and 3 chars")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }


        
    }
}
