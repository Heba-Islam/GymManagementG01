using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models
{
    public class CreateAMemberViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50,  ErrorMessage = "a namw must be between 2 and 50")]
        [MinLength(2, ErrorMessage = "a namw must be between 2 and 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage ="name must contain Letters or spaces only")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage ="email is required")]
        [EmailAddress(ErrorMessage ="invalid format")]
        [StringLength(100,MinimumLength = 5 , ErrorMessage = "email must be between 5 and 100")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "phone is mandatory")]
        [Phone(ErrorMessage = "invalid format")]
        [RegularExpression(@"^(0)(10|12|11|15)\d{8}$")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Birthday is mandatory")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "gender is mandatory")]

        public Gender Gender { get; set; }

        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "street is mandatory")]
        [StringLength(50,MinimumLength =2 ,ErrorMessage= "street must be be between 2 and 50 chars ")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "city is mandatory")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "city must be be between 2 and 50 chars ")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "healthRecord is required")]
        public HealthRecordViewModel HealthRecord { get; set; } = null!;


    }
}
