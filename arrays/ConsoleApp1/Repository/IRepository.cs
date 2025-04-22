namespace ConsoleApp1.Repository
{
    using ConsoleApp1.Entites;

    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IList<T> FindAll();

        List<T> GetByPredicate(Func<T, bool> predicate);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
