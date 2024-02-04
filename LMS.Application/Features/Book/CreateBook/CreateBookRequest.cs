using MediatR;

namespace LMS.Application.Features.BookFeatures.CreateBook;

public record CreateBookRequest(long Id, string Title, string Author, string ISBN,
                                string Genre, int PublishedYear, int AvailableCopies,
                                int TotalCopies, bool IsAvailable) : IRequest<CreateBookResponse>;
