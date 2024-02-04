using LMS.Application.Features.BookFeatures;
using LMS.Application.Features.Library.Command.AddBookToLibrary;
using LMS.Application.Features.Library.Command.RemoveBookFromLibrary;
using LMS.Application.Features.Library.Query.GetBookNotAdded;
using LMS.Application.Features.Library.Query.GetBooksNotAdded;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/libraries")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibrariesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-Added-books")]
        public async Task<ActionResult<LibraryBookAddedResponse>> GetAddesBooks(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var request = new LibraryBookAddedRequest(pageIndex, pageSize);

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-books-not-added")]
        public async Task<ActionResult<BookResponse>> GetNotAddedBooks(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var request = new BookNotAddedRequest(pageIndex, pageSize);

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("add-book-to-library")]
        public async Task<ActionResult<BookResponse>> AddLibraryBook(AddBookNotAddedRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("remove-book-from-library")]
        public async Task<ActionResult<bool>> RemoveLibraryBook(RemoveBookFromLibraryRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
