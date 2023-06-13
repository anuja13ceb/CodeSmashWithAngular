using CodeSmashWithAngular.Configurations;
using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = applicationDbContext.Set<T>();
        }

        public virtual IEnumerable<T> All()
        {
            return _dbSet.ToList();
        }
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            
        }
    }
}
