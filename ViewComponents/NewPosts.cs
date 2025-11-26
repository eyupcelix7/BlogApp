using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class NewPosts: ViewComponent
    {
        IPostRepository _postRepository { get; set; }

        public NewPosts(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(
                await _postRepository
                .Posts
                .OrderByDescending(p=> p.PublishedOn)
                .Take(10)
                .ToListAsync()
            );
        }
    }
}
