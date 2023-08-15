using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly hana_taekwondoContext _context;
    private readonly DbSet<T> _entities;

    public Repository(hana_taekwondoContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    public DbSet<T> GetDbSet() => _entities;

    public T Find(params object?[]? objects)
    {
        var findResult = _entities.Find(objects);
        return findResult ?? throw new NullReferenceException("Record not found");
    }

    public virtual IEnumerable<T> FindwithQuery(Expression<Func<T, bool>> predicate)
    {
        return _entities.Where(predicate);
    }

    public async Task<List<T>> GetAll()
    {
        var Results = await _entities.ToListAsync();
        return Results;
    }

    public async Task Add(T entity)
    {
        _entities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        if (_context.Entry<T>(entity) == null) throw new NullReferenceException("Record not found");
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        if (_context.Entry<T>(entity) == null) throw new NullReferenceException("Record not found");
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(params object?[]? key)
    {
        var foundRecord = Find(key);
        _entities.Remove(foundRecord);
        await _context.SaveChangesAsync();
    }
}