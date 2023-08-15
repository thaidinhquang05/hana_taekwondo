using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    public DbSet<T> GetDbSet();
    public Task<List<T>> GetAll();
    public T Find(params object?[]? objects);
    public Task Add(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
    public Task Delete(params object?[]? key);
}