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

    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter Name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "We need it to mail you")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Oops! looks like you mistyped")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Let us know your views")]
        [DataType(DataType.Text)]
        [Display(Name = "Thoughts")]
        public string Thoughts { get; set; }
    }
}
