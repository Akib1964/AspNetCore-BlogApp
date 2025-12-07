using BlogPosts.Data;
//using Comment.Models;
using BlogPosts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml.Linq;
namespace BlogPosts.Controllers
{
    public class CommentsController : Controller
    {
        private readonly BlogDbContext _context;
        public CommentsController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postId, string User, string UserComments)
        {
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(UserComments))
            {
                TempData["Error"] = "Name and Comment cannot be empty.";
                return RedirectToAction("Details", "BlogPost", new { Id = postId });
            }
            var comment = new Comment
            {
                PostId= postId,
                Author = User,
                Content = UserComments,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Comment added successfully.";
            return RedirectToAction("Details", "BlogPost", new { Id = postId });
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postId, string author, string content)
        {
            // simple validation
            if (string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Name and Comment cannot be empty.";
                return RedirectToAction("Details", "BlogPost", new { id = postId });
            }

            // create new comment object
            var comment = new Comment
            {
                PostId = postId,
                Author = author,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            // save to database
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Comment added successfully.";

            return RedirectToAction("Details", "BlogPost", new { id = postId });
        }
    }
}
