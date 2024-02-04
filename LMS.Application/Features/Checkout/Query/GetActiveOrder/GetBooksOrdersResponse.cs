namespace LMS.Application.Features.Checkout.Query.GetActiveOrder
{
    public class GetBooksOrdersResponse
    {
        public long Id { get; set; }
        public string BookName { get; set; }
        public string PatroName { get; set; }
        public DateTimeOffset checkoutDate { get; set; } 
    }
}
