using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Interfaces;
using CodeSmashWithAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(x=>x.Username == userName && x.Password== password);
        }
    }
}
