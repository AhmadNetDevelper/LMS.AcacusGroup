using LMS.Application.Common.PaginationRecord;
using LMS.Application.Repositories;
using LMS.Domain.Common;
using LMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DataContext Context;
    private bool _disposed;

    public BaseRepository(DataContext context)
    {
        Context = context;
    }

    public void Insert(T entity)
    {
        Context.Add(entity);
    }

    public void InsertRange(List<T> entities)
    {
        Context.AddRange(entities);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void UpdateRange(List<T> entities)
    {
        Context.UpdateRange(entities);
    }

    public void Delete(T entity)
    {
        Context.Remove(entity);
    }

    public void DeleteRange(List<T> entity)
    {
        Context.RemoveRange(entity);
    }

    public Task<T> Get(long id, CancellationToken cancellationToken)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PaginationRecord<T>> GetAll(CancellationToken cancellationToken)
    {
        return new PaginationRecord<T>
        {
            DataRecord = await Context.Set<T>().ToListAsync(cancellationToken),
            CountRecord = await Context.Set<T>().CountAsync(cancellationToken),
        };
    }

    public async Task<PaginationRecord<T>> GetAll(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return new PaginationRecord<T>
        {
            DataRecord = await Context.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken),
            CountRecord = await Context.Set<T>().CountAsync(cancellationToken),
        };
    }

    public void Dispose()
    {
        try
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        catch (Exception)
        {
            throw new Exception("Error When Dispose");
        }
    }

    public virtual void Dispose(bool disposing)
    {
        try
        {
            if (!_disposed && disposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }
        catch (Exception)
        {
            throw new Exception("Error When Dispose");
        }
    }
}