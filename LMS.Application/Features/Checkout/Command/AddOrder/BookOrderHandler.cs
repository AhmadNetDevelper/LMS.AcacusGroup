using AutoMapper;
using LMS.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LMS.Application.Features.Checkout.Command.AddOrder
{
    public class BookOrderHandler : IRequestHandler<BookCheckoutRequest, BookOrderResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatronRepository _patronRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IMapper _mapper;

        public BookOrderHandler(IPatronRepository patronRepository, IBookRepository bookRepository,
                                IUnitOfWork unitOfWork, IMapper mapper, ICheckoutRepository checkoutRepository, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _patronRepository = patronRepository;
            _bookRepository = bookRepository;
            _checkoutRepository = checkoutRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BookOrderResponse> Handle(BookCheckoutRequest request, CancellationToken cancellationToken)
        {

            var book = await _bookRepository.Get(request.bookId, cancellationToken);
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var patron = await _patronRepository.GetByUserId(userId, cancellationToken);

            var isPassMaxReserve = await _checkoutRepository.IsPassMaxReservations(patron.Id, cancellationToken);

            if (isPassMaxReserve)
            {
                return new BookOrderResponse
                {
                    IsSucceeded = false,
                    ResultMessage = "You have exceeded the maximum reservation limit."
                };
            }

            var isLateReturning = await _checkoutRepository.IsLateReturningBook(patron.Id, cancellationToken);

            if (isLateReturning)
            {
                return new BookOrderResponse
                {
                    IsSucceeded = false,
                    ResultMessage = "You was late delivering some books."
                };
            }

            if (book != null && patron != null)
            {
                _checkoutRepository.Insert(new Domain.Entities.Checkout
                {
                    BookId = book.Id,
                    CheckoutDate = DateTime.Now,
                    PatronId = patron.Id,
                    DueDate = DateTime.Now.AddDays(3),
                    IsReturned = false,
                });
            }

            await _unitOfWork.Save(cancellationToken);

            return new BookOrderResponse { IsSucceeded = true, ResultMessage = "successful" };
        }
    }
}
