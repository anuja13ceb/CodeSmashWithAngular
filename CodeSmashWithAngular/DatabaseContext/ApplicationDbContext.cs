using CodeSmashWithAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.DatabaseContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUI> UserUIs { get; set; }
    }
}
