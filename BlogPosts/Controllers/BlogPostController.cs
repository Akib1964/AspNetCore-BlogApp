using BlogPosts.Data;
using BlogPosts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPosts.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly BlogDbContext _context;
        public BlogPostController(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Account");
            }
            var blogPost = await _context.BlogPosts
                .Include(p => p.User)
                .ToListAsync();
            return View(blogPost);
        }
        [HttpGet]
        public IActionResult Create()
        {
            BlogPost post = new BlogPost
            {
                PublishedDate = DateTime.Today
            };
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost Blogdetails)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Account");
            }
            //Blogdetails.UserId = 1;  // demo user
            //get the user login details
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userdetails = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            Blogdetails.UserId = userdetails.Id;

            ModelState.Remove("User");
            ModelState.Remove("Comments");

            if (ModelState.IsValid)
            {
                _context.Add(Blogdetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(Blogdetails);
        }
       /** public async Task<IActionResult> Details(int id)
        {
            return View();
        }*/
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blogpost = await _context.BlogPosts.FindAsync(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            return View(blogpost);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost blogDetails)
        {
            if (id != blogDetails.Id)
            {
                return NotFound();
            }
            blogDetails.UserId = 1;
            ModelState.Remove("User");
            ModelState.Remove("Comments");

            if (ModelState.IsValid)
            {
                _context.Update(blogDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var blogDetails = await _context.BlogPosts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (blogDetails == null)
            {
                return NotFound();
            }
            return View(blogDetails);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var blogDetails = await _context.BlogPosts.FindAsync(Id);
            if (blogDetails == null)
            {
                return NotFound();
            }
            _context.BlogPosts.Remove(blogDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult>  Details(int id,string returnUrl=null)
        {
            var blogDetails = await _context.BlogPosts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
            if(blogDetails==null)
            {
                return NotFound();
            }
            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "BlogPost");
            return View(blogDetails);

        }
    }
}
