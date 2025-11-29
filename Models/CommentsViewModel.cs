using BlogApp.Entity;

namespace BlogApp.Models
{
    public class CommentsViewModel
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime PublishedOn { get; set; }
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}
