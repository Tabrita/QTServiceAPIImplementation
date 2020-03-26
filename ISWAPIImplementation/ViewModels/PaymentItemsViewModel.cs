using ISWAPIImplementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISWAPIImplementation.ViewModels
{
    public class PaymentItemsViewModel
    {
        public IEnumerable<PaymentItems> PaymentItemsVM { get; set; }
    }
}
