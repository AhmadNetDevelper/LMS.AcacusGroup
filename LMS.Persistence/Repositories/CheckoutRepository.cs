using LMS.Application.Common.PaginationRecord;
using LMS.Application.Features.Checkout.Query.GetActiveOrder;
using LMS.Application.Repositories;
using LMS.Domain.Entities;
using LMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace LMS.Persistence.Repositories
{
    public class CheckoutRepository : BaseRepository<Checkout>, ICheckoutRepository
    {
        public CheckoutRepository(DataContext context) : base(context)
        {

        }

        public async Task<bool> IsPassMaxReservations(long patronId, CancellationToken cancellationToken)
        {
            var activeReservationsCount = await Context.Checkouts.Where(x => x.PatronId == patronId
                                                                  && x.IsApproved == false
                                                                  ).CountAsync(cancellationToken);

            return await Task.FromResult(activeReservationsCount >= 3);
        }


        public async Task<bool> IsLateReturningBook(long patronId, CancellationToken cancellationToken)
        {
            var isLateReturningBook = await Context.Checkouts.Where(x => x.PatronId == patronId
                                                                  && x.IsApproved == true
                                                                  && x.IsReturned == false
                                                                  && DateTime.Now > x.DueDate
                                                                  ).CountAsync(cancellationToken);
            return await Task.FromResult(isLateReturningBook > 0);
        }

        public async Task<PaginationRecord<GetBooksOrdersResponse>> GetBooksOrdersNeedApproval(GetBookOrderRequest orderRequest, CancellationToken cancellationToken)
        {
            var boksOrder = await Context.Checkouts.Include(x => x.Book)
                                    .Include(x => x.Patron)
                                    .Where(x => x.IsApproved == false)
                                    .Skip((orderRequest.pageIndex - 1) * orderRequest.pageSize)
                                    .Take(orderRequest.pageSize)
                                    .Select(o => new GetBooksOrdersResponse
                                    {
                                        Id = o.Id,
                                        BookName = o.Book.Title,
                                        PatroName = o.Patron.FirstName,
                                        checkoutDate = o.CheckoutDate
                                    }).ToListAsync(cancellationToken);

            var orderCounts = await Context.Checkouts.Include(x => x.Book)
                                    .Include(x => x.Patron)
                                    .Where(x => x.IsApproved == false).CountAsync(cancellationToken);


            return new PaginationRecord<GetBooksOrdersResponse>()
            {
                DataRecord = boksOrder,
                CountRecord = orderCounts
            };


        }
    }
}
