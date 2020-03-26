using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISWAPIImplementation.Models
{
    public class SuccessFailPage
    {
        public string transactionRef { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string responseCodeGrouping { get; set; }
    }
}
