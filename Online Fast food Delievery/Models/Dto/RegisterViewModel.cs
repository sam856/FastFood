using System.ComponentModel.DataAnnotations;

namespace Online_Fast_food_Delievery.Models.Dto
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public string Name { get; set; }
        public string  City { get; set; }
        public string Address { get; set; }
        public string  PostalCode { get; set; }
    }
}
