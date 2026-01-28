using Microsoft.EntityFrameworkCore;
using RenderApp.Entity;

namespace RenderApp 
{
    public class RenderAppDbContext(DbContextOptions<RenderAppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}
