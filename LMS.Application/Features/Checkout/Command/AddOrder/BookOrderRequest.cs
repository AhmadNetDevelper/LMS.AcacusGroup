using MediatR;

namespace LMS.Application.Features.Checkout.Command.AddOrder
{
    public record BookCheckoutRequest(long bookId) : IRequest<BookOrderResponse>;
}
