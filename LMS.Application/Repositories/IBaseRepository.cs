using LMS.Application.Common.PaginationRecord;
using LMS.Domain.Common;
using System.Linq.Expressions;

namespace LMS.Application.Repositories;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    void Insert(T entity);
    void InsertRange(List<T> listEntities);
    void Update(T entity);
    void UpdateRange(List<T> entity);
    void Delete(T entity);
    void DeleteRange(List<T> listEntities);
    Task<T> Get(long id, CancellationToken cancellationToken);
    Task<PaginationRecord<T>> GetAll(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<PaginationRecord<T>> GetAll(CancellationToken cancellationToken);
}