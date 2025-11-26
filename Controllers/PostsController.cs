using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.Controllers
{
    public class PostsController: Controller
    {
        private IPostRepository _postRepository;
        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IActionResult> Index(string tag)
        {
            IQueryable<Post> posts = _postRepository.Posts;
            if(!tag.IsNullOrEmpty())
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }
            return View(
                new PostsViewModel
                {
                    Posts = await posts.ToListAsync()
                }    
            );
        }
        public async Task<IActionResult> Details(string url)
        {
            return View(
                await _postRepository
                .Posts
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .ThenInclude(x=> x.User)
                .FirstOrDefaultAsync(p=> p.Url == url)
            );
        }
        public IActionResult AddComment(int PostId, string UserName,string Text)
        {
            return View();
            //return RedirectToAction();
        }
    }
}
