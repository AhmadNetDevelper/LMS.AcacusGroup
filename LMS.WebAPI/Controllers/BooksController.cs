using LMS.Application.Features.BookFeatures;
using LMS.Application.Features.BookFeatures.CreateBook;
using LMS.Application.Features.BookFeatures.GetBook;
using LMS.Application.Features.BookFeatures.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-books")]
        public async Task<ActionResult<BookResponse>> GetBooks(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var request = new GetBookRequest(pageIndex,pageSize);

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("create-book")]
        public async Task<ActionResult<CreateBookResponse>> Create(CreateBookRequest request,
       CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update-book")]
        public async Task<ActionResult<UpdateBookResponse>> Update(UpdateBookRequest request,
       CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

    }
}
