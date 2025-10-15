using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.PlanViewModels
{
    public class PlanToUpdateViewModel
    {
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 And 50 Characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is mandatory")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 100 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration Day is madatory")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and a year")]
        public int DurationDays { get; set; }


        [Required(ErrorMessage = "Price is madatory")]
        [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
