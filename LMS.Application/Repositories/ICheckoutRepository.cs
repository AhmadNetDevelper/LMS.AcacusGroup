using LMS.Application.Common.PaginationRecord;
using LMS.Application.Features.Checkout.Query.GetActiveOrder;
using LMS.Domain.Entities;

namespace LMS.Application.Repositories
{
    public interface ICheckoutRepository : IBaseRepository<Checkout>
    {
        Task<bool> IsPassMaxReservations(long patronId, CancellationToken cancellationToken);
        Task<bool> IsLateReturningBook(long patronId, CancellationToken cancellationToken);
        Task<PaginationRecord<GetBooksOrdersResponse>> GetBooksOrdersNeedApproval(GetBookOrderRequest orderRequest, CancellationToken cancellationToken);
    }
}
