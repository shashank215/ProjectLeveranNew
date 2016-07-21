using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLeveran.Models
{



    public class BookServiceViewModel
    {
        [Required(ErrorMessage = "Please enter Name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter valid Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter valid Contact Number")]
        [Display(Name = "Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid Contact Number")]
        public string Number { get; set; }
    }
}
