using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    DbSet<T> GetDbSet();

    Task<List<T>> GetAll();

    Task Add(T entity);

    Task Update(T entity);

    Task Delete(T entity);
}