using ISWAPIImplementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISWAPIImplementation.ViewModels
{
    public class BillerViewModel
    {
        public IEnumerable<Billers> Billers { get; set; }
        public int BillerPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Billers.Count() / (double)BillerPerPage));
        }
        public IEnumerable<Billers> PaginatedBillers()
        {
            int start = (CurrentPage - 1) * BillerPerPage;
            return Billers.OrderBy(b => b.BillerId).Skip(start).Take(BillerPerPage);
        }
    }
}
