using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!;

        public string? Photo { get; set; }

        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "invalid format")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "email must be between 5 and 100")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "phone is mandatory")]
        [Phone(ErrorMessage = "invalid format")]
        [RegularExpression(@"^(0)(10|12|11|15)\d{8}$")]
        public string Phone { get; set; } = null!;

        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "street is mandatory")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "street must be be between 2 and 50 chars ")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "city is mandatory")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "city must be be between 2 and 50 chars ")]
        public string City { get; set; } = null!;

    }
}
