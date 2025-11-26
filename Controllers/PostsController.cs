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
        private ICommentRepository _commentRepository;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index(string tag)
        {
            IQueryable<Post> posts = _postRepository.Posts;
            var claims = User.Claims;
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
        [HttpPost]
        public IActionResult AddComment(int PostId, string UserName,string Text,string Url)
        {
            Comment comment = new Comment
            {
                CommentText = Text,
                PostId = PostId,
                PublishedOn = DateTime.Now,
                User = new User
                {
                    UserName = UserName,
                    Image = "eyupcelix7.jpg"
                }
            };
            _commentRepository.CreateComment(comment);
            //return Redirect($"/posts/details/{Url}");
            return RedirectToRoute("post_details", new {url = Url});
        }
    }
}
