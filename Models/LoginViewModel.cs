using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Display(Name = "E-Posta")]
        [Required]
        public string? Email { get; set; }
        [Required]
        [StringLength(10,ErrorMessage = "{0} alanı maksimum {1} karakter, minimum {2} karakter olmalıdır.",MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string? Password { get; set; }
    }
}
