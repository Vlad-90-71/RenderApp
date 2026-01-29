using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderApp.Pages.User
{
    public class DeleteModel(RenderAppDbContext context, IWebHostEnvironment env) : PageModel
    {
        private readonly RenderAppDbContext _context = context;
        private readonly IWebHostEnvironment _env = env;

        public Entity.User? EntityUser { get; set; }

        public void OnGet(int id)
        {
            EntityUser = _context.Users.Find(id);
        }

        public IActionResult OnPost(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            
            // если у пользовател€ есть фото Ч удал€ем файл
            if (!string.IsNullOrEmpty(user.PhotoPath)) 
            { 
                var filePath = Path.Combine(_env.WebRootPath, user.PhotoPath.TrimStart('/'));
                if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath); 
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
