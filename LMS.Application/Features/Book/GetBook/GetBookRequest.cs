using LMS.Application.Common.PaginationRecord;
using LMS.Application.Features.BookFeatures.CreateBook;
using MediatR;

namespace LMS.Application.Features.BookFeatures.GetBook
{
    public record GetBookRequest(int pageIndex, int pageSize) : IRequest<PaginationRecord<BookResponse>>;
}
