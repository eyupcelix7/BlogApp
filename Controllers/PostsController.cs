using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

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
        [HttpGet]
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
        [HttpGet]
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
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                _postRepository.CreatePost(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Description = model.Description,
                        Url = model.Url,
                        UserId = 1,
                        PublishedOn= DateTime.Now,
                        Image = "eyupcelix7.jpg",
                        IsActive = true
                    }
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            string userName = User.FindFirstValue(ClaimTypes.Name) ?? "";
            if(!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            IQueryable<Post> posts = _postRepository.Posts;
            return View(await posts.ToListAsync());
        }

    }
}
