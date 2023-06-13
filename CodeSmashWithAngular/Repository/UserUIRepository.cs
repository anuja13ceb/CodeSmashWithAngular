using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Interfaces;
using CodeSmashWithAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.Repository
{
    public class UserUIRepository : GenericRepository<UserUI>, IUserUIRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserUIRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool CheckEmailExist(string? email)
        {
            return _applicationDbContext.UserUIs.Any(x => x.Email == email);
        }

        public bool CheckUsernameExist(string? username)
        {
            return _applicationDbContext.UserUIs.Any(x => x.Email == username);
        }
    }
}
