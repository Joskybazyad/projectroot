using System.ComponentModel.DataAnnotations;

namespace projectroot.ViewModels.Account
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Is Nessecary")]
        public string Email { get; set; }
    }
}
