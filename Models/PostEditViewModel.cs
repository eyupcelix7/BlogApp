using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class PostEditViewModel
    {
        [Required(ErrorMessage = "ID alanını Lütfen Boş Bırakmayınız.")]
        [Display(Name = "ID")]
        public int PostId { get; set; }
        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; }
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

    }
}
