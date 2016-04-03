using System.ComponentModel.DataAnnotations;

namespace bdlSecurity.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20, MinimumLength =1)]
        public string UserName { get; set; }
        [Required]
        public int Password { get; set; }
    }
}