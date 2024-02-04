using LMS.Application.Common.PaginationRecord;
using LMS.Domain.Entities;

namespace LMS.Application.Repositories
{
    public interface ILibraryRepository : IBaseRepository<Library>
    {
        Task<PaginationRecord<Book>> GetLibraryBooks(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<PaginationRecord<Book>> GetLibraryBooksNotAdded(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
