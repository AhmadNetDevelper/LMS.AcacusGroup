using MediatR;

namespace LMS.Application.Features.Library.Command.RemoveBookFromLibrary
{
    public record RemoveBookFromLibraryRequest(long bookId) : IRequest<bool>;
}
