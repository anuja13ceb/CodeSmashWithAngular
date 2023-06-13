using CodeSmashWithAngular.Models;

namespace CodeSmashWithAngular.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Authenticate(string userName, string password);
    }
}
