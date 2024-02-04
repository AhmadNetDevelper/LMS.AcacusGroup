using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Features.Checkout.Command.AddOrder
{
    public class BookOrderResponse
    {
        public bool IsSucceeded { get; set; }
        public string ResultMessage { get; set; }
    }
}
