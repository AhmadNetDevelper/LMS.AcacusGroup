using AutoMapper;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Checkout.Command.AcceptOrder
{
    internal class AcceptOrderHandler : IRequestHandler<AcceptOrderRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IMapper _mapper;

        public AcceptOrderHandler(ICheckoutRepository checkoutRepository, IUnitOfWork unitOfWork)
        {
            _checkoutRepository = checkoutRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AcceptOrderRequest request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.Get(request.orderId,cancellationToken);

            if (checkout == null)
            {
                return await Task.FromResult(false);
            }

            checkout.IsApproved = true;
            checkout.CheckoutDate = DateTime.Now;
            checkout.DueDate = DateTime.Now.AddDays(3);

            await _unitOfWork.Save(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
