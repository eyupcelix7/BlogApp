using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.Controllers
{
    public class TagsController : Controller
    {
        ITagRepository _tagsRepository;
        public TagsController(ITagRepository tagRepository)
        {
            _tagsRepository = tagRepository;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Index(int? Id)
        {
            IQueryable<Tag> tags = _tagsRepository.Tags;
            if (Id != null)
            {
                Tag? findTag = _tagsRepository.Tags.Include(x=> x.Posts).FirstOrDefault(x=> x.TagId == Id);
                if(findTag != null)
                {
                    return View(
                        new TagsViewModel
                        {
                            TagId = findTag.TagId,
                            Tags = tags.ToList(),
                            Text = findTag.Text,
                            Color = findTag.Color,
                            Url = findTag.Url,
                            Posts = findTag.Posts,
                            Edit = true
                        }
                    );
                }
            }
            return View(
                new TagsViewModel
                {
                    Tags = tags.ToList(),
                    Edit = false
                }
            );
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(TagsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _tagsRepository.CreateTag(new Tag
                {
                    Color = model.Color,
                    Text = model.Text!.ToLower(),
                    Url = model.Url
                });
                return RedirectToAction("Index");
            }
            return View("Index",model);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Tag? findTag = _tagsRepository.Tags.FirstOrDefault(x => x.TagId == Id);
            if(findTag == null)
            {
                return NotFound();
            }
            _tagsRepository.DeleteTag(findTag);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize]
        public IActionResult Update(Tag tag)
        {
            if(tag == null)
            {
                return NotFound();
            }
            _tagsRepository.UpdateTag(tag);
            return RedirectToAction("Index");
        }
    }
}
