using MediatR;

namespace LMS.Application.Features.Library.Command.AddBookToLibrary
{
    public record AddBookNotAddedRequest(long bookId) : IRequest<bool>;
}
