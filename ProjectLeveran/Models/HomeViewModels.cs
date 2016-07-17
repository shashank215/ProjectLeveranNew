using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLeveran.Models
{



    public class BookServiceViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Number")]
        public string Number { get; set; }
    }
}
