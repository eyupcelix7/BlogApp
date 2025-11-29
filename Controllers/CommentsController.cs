using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class CommentsController: Controller
    {
        ICommentRepository _commentRepository;
        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View(_commentRepository.Comments.Include(x=> x.Post).Include(x=> x.User).ToList());
        }
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Comment? findComment = _commentRepository.Comments.FirstOrDefault(x=> x.CommentId == Id);
            if(findComment == null)
            {
                return NotFound();
            }
            _commentRepository.DeleteComment(findComment);
            return RedirectToAction("Index");
        }
    }
}
