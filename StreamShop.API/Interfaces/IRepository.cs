namespace StreamShop.API.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Delete(T entityToDelete);
    void Update(T entityToUpdate, T entity);
}