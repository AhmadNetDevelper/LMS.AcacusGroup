using LMS.Application.Common.PaginationRecord;
using MediatR;

namespace LMS.Application.Features.Library.Query.GetBooksNotAdded
{
    public record LibraryBookAddedRequest(int pageIndex, int pageSize) : IRequest<PaginationRecord<LibraryBookAddedResponse>>;
}
