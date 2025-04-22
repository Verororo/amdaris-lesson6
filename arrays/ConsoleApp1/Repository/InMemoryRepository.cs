using ConsoleApp1.Entites;
using ConsoleApp1.Repository;

public class InMemoryRepository<T> : IRepository<T> where T : Entity
{
    private readonly List<T> _entities = new List<T>();
    private int _nextId = 1;

    public T GetById(int id)
    {
        return _entities.FirstOrDefault(e => e.Id == id);
    }

    public IList<T> FindAll()
    {
        return _entities.ToList();
    }

    public void Add(T entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _nextId++;
        }
        _entities.Add(entity);
    }

    public void Delete(T entity)
    {
        _entities.RemoveAll(e => e.Id == entity.Id);
    }

    public void Update(T entity)
    {
        var existingEntity = GetById(entity.Id);
        if (existingEntity != null)
        {
            _entities.Remove(existingEntity);
            _entities.Add(entity);
        }
    }

    public List<T> GetByPredicate(Func<T, bool> predicate)
    {
        return _entities.Where(predicate).ToList();
    }
}
