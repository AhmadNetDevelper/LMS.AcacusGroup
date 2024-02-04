using MediatR;

namespace LMS.Application.Features.BookFeatures.UpdateBook;
public record UpdateBookRequest(long Id, string Title, string Author, string ISBN,
                                  string Genre, int PublishedYear, int AvailableCopies,
                                  int TotalCopies, bool IsAvailable) : IRequest<UpdateBookResponse>;

