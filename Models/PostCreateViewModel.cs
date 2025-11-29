using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class PostCreateViewModel
    {
        [Required(ErrorMessage = "Başlık alanını Lütfen Boş Bırakmayınız.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = null!;
        [Display(Name = "Kısa Açıklama")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "İçerik alanını Lütfen Boş Bırakmayınız.")]
        [Display(Name = "İçerik")]
        public string Content { get; set; } = null!;
        [Required(ErrorMessage = "Kısa Url alanını Lütfen Boş Bırakmayınız.")]
        [Display(Name = "Kısa Url")]
        public string Url { get; set; } = null!;
        [DataType(DataType.Upload)]
        [Display(Name = "Resim")]
        public IFormFile? Image { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
