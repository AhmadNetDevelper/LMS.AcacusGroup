using LMS.Application.Common.PaginationRecord;
using LMS.Domain.Entities;

namespace LMS.Application.Repositories
{
    public interface IPatronRepository : IBaseRepository<Patron>
    {
        Task<Patron> GetByUserId(long userId, CancellationToken cancellationToken);

    }
}
