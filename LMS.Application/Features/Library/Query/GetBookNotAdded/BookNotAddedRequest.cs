using LMS.Application.Common.PaginationRecord;
using LMS.Application.Features.Library.Query.GetBooksNotAdded;
using MediatR;

namespace LMS.Application.Features.Library.Query.GetBookNotAdded
{
    public record BookNotAddedRequest(int pageIndex, int pageSize) : IRequest<PaginationRecord<LibraryBookAddedResponse>>;
}
