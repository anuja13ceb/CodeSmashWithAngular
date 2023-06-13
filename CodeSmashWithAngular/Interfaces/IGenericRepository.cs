namespace CodeSmashWithAngular.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> All();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
      
    }
}
