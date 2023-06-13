using CodeSmashWithAngular.Models;

namespace CodeSmashWithAngular.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        IEnumerable<City> GetAll();
    }
}
