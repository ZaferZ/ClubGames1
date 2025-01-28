using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _8BallPool.ViewModels
{
    public class LoginVM
    {

        [DisplayName("Username: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string UserName { get; set; }

        [DisplayName("Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Password { get; set; }
    }
}
