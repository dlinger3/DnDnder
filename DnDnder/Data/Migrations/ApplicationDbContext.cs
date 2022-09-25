using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tavern.Models;

namespace Tavern.Data.Migrations
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; } = default!;
        public DbSet<Campaign> Campaign { get; set; }

        public DbSet<Character>? Character { get; set; }
    }
}