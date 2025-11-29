using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class TagsViewModel
    {
        public int TagId { get; set; }
        [Required(ErrorMessage = "Etiket Adı Kısımı Boş Olmamalıdır.")]
        [Display(Name = "Etiket Adı")]
        [StringLength(50, ErrorMessage = "En Fazla 20 Karakter Olmalıdır.")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "Etiket URL Kısımı Boş Olmamalıdır.")]
        [Display(Name = "Etiket Url")]
        [StringLength(50, ErrorMessage = "En Fazla 25 Karakter Olmalıdır.")]
        public string? Url { get; set; }
        [Display(Name = "Etiket Renk")]
        public TagColors? Color { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Post> Posts { get; set; } = new List<Post>();
        public bool Edit { get; set; }
    }
}
