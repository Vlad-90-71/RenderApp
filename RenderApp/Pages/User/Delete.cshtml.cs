using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderApp.Pages.User
{
    public class DeleteModel(RenderAppDbContext context) : PageModel
    {
        private readonly RenderAppDbContext _context = context;

        public Entity.User? EntityUser { get; set; }

        public void OnGet(int id)
        {
            EntityUser = _context.Users.Find(id);
        }

        public IActionResult OnPost(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
