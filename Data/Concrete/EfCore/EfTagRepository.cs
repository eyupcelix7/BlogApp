using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfTagRepository : ITagRepository
    {
        BlogContext _context;
        public EfTagRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void DeleteTag(Tag tag)
        {
            _context.Remove(tag);
            _context.SaveChanges();
        }

        public void UpdateTag(Tag tag)
        {
            Tag? findTag = _context.Tags.Include(x => x.Posts).FirstOrDefault(x => x.TagId == tag.TagId);
            if (findTag != null)
            {
                findTag.Text = tag.Text;
                findTag.Url = tag.Url;
                findTag.Color = tag.Color;
                _context.SaveChanges(true);
            }
        }
    }
}
