using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Interfaces;
using CodeSmashWithAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.Repository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CityRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;
        }

        public IEnumerable<City> GetAll()
        {
            return _applicationDbContext.Cities.ToList();
        }
    }
}
