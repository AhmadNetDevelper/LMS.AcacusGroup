using LMS.Application.Common.PaginationRecord;
using MediatR;

namespace LMS.Application.Features.Checkout.Query.GetActiveOrder
{
    public record GetBookOrderRequest(int pageIndex, int pageSize) : IRequest<PaginationRecord<GetBooksOrdersResponse>>;
}
