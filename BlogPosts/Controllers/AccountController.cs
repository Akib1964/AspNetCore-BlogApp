using BlogPosts.Data;
using BlogPosts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
namespace BlogPosts.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogDbContext _context;
        public AccountController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(await _context.Users.AnyAsync(u=>u.Email==model.Email))
            {
                ModelState.AddModelError("Email","Email is already registerd!!!");
                return View(model);

            }
            string passwordHash = HashPassword(model.Password);
            var user = new User
            {
                Name= model.Name,
                Email= model.Email,
                PasswordHash= passwordHash
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            //stor session for login
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Index", "Home");
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes=sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            // Correct condition
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid Email or Password!!!");
                return View(model);
            }

            // Store session after successful login
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string enteredPassword, string storedHashPassword)
        {
            return HashPassword(enteredPassword) == storedHashPassword;
        }

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Find user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            // Check user + password
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid Email or Password!!!");
                return View(model);
            }

            // Store session
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);

            // 🔥 Return to previous page if available
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Default redirect
            return RedirectToAction("Index", "Home");
        }
        private bool VerifyPassword(string enteredPassword, string storedHashPassword)
        {
            return HashPassword(enteredPassword) == storedHashPassword;
        }*/



        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
