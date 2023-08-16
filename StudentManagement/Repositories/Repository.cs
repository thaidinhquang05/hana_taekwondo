using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly hana_taekwondoContext _context;
    private readonly DbSet<T> _entities;

    public Repository(hana_taekwondoContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    public virtual DbSet<T> GetDbSet() => _entities;

    public virtual async Task<List<T>> GetAll()
    {
        var results = await _entities.ToListAsync();
        return results;
    }

    public virtual async Task Add(T entity)
    {
        _entities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Update(T entity)
    {
        if (_context.Entry<T>(entity) == null) throw new NullReferenceException("Record not found");
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Delete(T entity)
    {
        if (_context.Entry<T>(entity) == null) throw new NullReferenceException("Record not found");
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}