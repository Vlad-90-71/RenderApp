using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderApp.Pages.User
{
    public class CreateModel(RenderAppDbContext context) : PageModel
    {
        private readonly RenderAppDbContext _context = context;

        [BindProperty]
        public Entity.User NewUser { get; set; } = new();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _context.Users.Add(NewUser);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
