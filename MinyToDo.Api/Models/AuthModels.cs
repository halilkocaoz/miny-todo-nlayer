using System.ComponentModel.DataAnnotations;

namespace MinyToDo.Api.Models.Auth
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Please, pass your username or email address")]
        public string Identifier { get; set; } // username or email

        [Required(ErrorMessage = "Please, pass your password")]
        public string Password { get; set; }
    }
    public class SignUpModel
    {
        [Required(ErrorMessage = "Please, pass your email adress")]
        [EmailAddress(ErrorMessage = "Please, pass your correct e-mail adress")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please, pass your Username")]
        [MaxLength(25, ErrorMessage = "Please, put maximum 25 character")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please, pass your password")]
        [MinLength(4, ErrorMessage = "Please, put minimum 4 character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, pass your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, pass your surname")]
        public string Surname { get; set; }
    }
}