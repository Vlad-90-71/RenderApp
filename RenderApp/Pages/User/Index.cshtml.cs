using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderApp.Pages.User
{
    public class IndexModel(RenderAppDbContext context) : PageModel
    {
        private readonly RenderAppDbContext _context = context;
        public List<Entity.User> Users { get; set; } = [];

        public void OnGet()
        {
            Users = [.. _context.Users];
        }
    }
}

