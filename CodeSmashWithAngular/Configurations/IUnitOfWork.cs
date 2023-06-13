using CodeSmashWithAngular.Interfaces;

namespace CodeSmashWithAngular.Configurations
{
    public interface IUnitOfWork
    {
        ICityRepository cityRepository { get; }
        IUserRepository userRepository{get;}
        IUserUIRepository userUIRepository { get; }
        int SaveChanges();


    }
}
