using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderApp.Pages.User
{
    public class CreateModel(RenderAppDbContext context, IWebHostEnvironment env) : PageModel
    {
        private readonly RenderAppDbContext _context = context;
        private readonly IWebHostEnvironment _env = env;

        [BindProperty]
        public Entity.User NewUser { get; set; } = new();
        
        [BindProperty] 
        public IFormFile? UploadPhoto { get; set; }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (UploadPhoto != null) 
            { 
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads"); 
                Directory.CreateDirectory(uploadsFolder); 

                var fileName = Guid.NewGuid() + Path.GetExtension(UploadPhoto.FileName); 
                var filePath = Path.Combine(uploadsFolder, fileName); 

                using var stream = new FileStream(filePath, FileMode.Create); 
                UploadPhoto.CopyTo(stream); 

                NewUser.PhotoPath = "/uploads/" + fileName; 
            }

            _context.Users.Add(NewUser);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
