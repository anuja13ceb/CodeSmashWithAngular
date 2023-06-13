using CodeSmashWithAngular.Models;

namespace CodeSmashWithAngular.Interfaces
{
    public interface IUserUIRepository : IGenericRepository<UserUI>
    {
        bool CheckEmailExist(string? email);
        bool CheckUsernameExist(string? username);




    }
}
