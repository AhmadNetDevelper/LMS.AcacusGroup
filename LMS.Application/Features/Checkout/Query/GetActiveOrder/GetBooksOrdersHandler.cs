using LMS.Application.Common.PaginationRecord;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Checkout.Query.GetActiveOrder
{
    public class GetBooksOrdersHandler : IRequestHandler<GetBookOrderRequest, PaginationRecord<GetBooksOrdersResponse>>
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetBooksOrdersHandler(ICheckoutRepository checkoutRepository, IUnitOfWork unitOfWork) 
        {
            _checkoutRepository = checkoutRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationRecord<GetBooksOrdersResponse>> Handle(GetBookOrderRequest request, CancellationToken cancellationToken)
        {
            return await _checkoutRepository.GetBooksOrdersNeedApproval(request, cancellationToken);
        }
    }
}
