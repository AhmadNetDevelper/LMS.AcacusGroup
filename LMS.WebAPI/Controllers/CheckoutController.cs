using LMS.Application.Common.PaginationRecord;
using LMS.Application.Features.Checkout.Command.AcceptOrder;
using LMS.Application.Features.Checkout.Command.AddOrder;
using LMS.Application.Features.Checkout.Query.GetActiveOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheckoutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("checkout-book")]
        public async Task<ActionResult<bool>> CheckoutBook(BookCheckoutRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-orders-books-need-approval")]
        public async Task<ActionResult<PaginationRecord<GetBooksOrdersResponse>>> OrdersNeedApproval(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var getBookOrderRequest = new GetBookOrderRequest(pageIndex, pageSize);
            var response = await _mediator.Send(getBookOrderRequest, cancellationToken);
            return Ok(response);
        }

        [HttpPost("approve-order-book")]
        public async Task<ActionResult<bool>> ApproveOrder(AcceptOrderRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
