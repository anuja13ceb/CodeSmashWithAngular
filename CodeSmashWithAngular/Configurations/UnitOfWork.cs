using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Interfaces;
using CodeSmashWithAngular.Models;
using CodeSmashWithAngular.Repository;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.Configurations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
         //public  ICityRepository _ICityRepository { get; }
       public readonly ICityRepository _ICityRepository;
        public readonly IUserRepository _userRepository;
        public readonly IUserUIRepository _userUIRepository;




        public UnitOfWork(ICityRepository iCityRepository, IUserRepository userRepository, IUserUIRepository userUIRepository, ApplicationDbContext context ) {
            this._ICityRepository = iCityRepository;
            this._userRepository = userRepository;
            this._userUIRepository=userUIRepository;
            this._context = context;
            
        }

        public ICityRepository cityRepository => new CityRepository(_context);

        public IUserRepository userRepository => new UserRepository(_context) ;

        public IUserUIRepository userUIRepository => new UserUIRepository(_context);

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
