using MediatR;

namespace LMS.Application.Features.Checkout.Command.AcceptOrder
{
    public record AcceptOrderRequest(long orderId) : IRequest<bool>;
}
