using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;
        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            var entity = _context.Posts.FirstOrDefault(x=> x.PostId == post.PostId);
            if(entity != null)
            {
                _context.Posts.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void UpdatePost(Post post)
        {
            var entity = _context.Posts.Include(x=> x.Tags).FirstOrDefault(i => i.PostId == post.PostId);
            if (entity != null)
            {
                entity.Description = post.Description;
                entity.Title = post.Title;
                entity.Url = post.Url;
                entity.Content = post.Content;
                entity.IsActive = post.IsActive;
                entity.Tags = post.Tags;
                _context.SaveChanges();
            }
        }
    }
}
