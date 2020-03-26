using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISWAPIImplementation.Models
{
    public class Billers
    {
        public string CategoryId { set; get; }
        public string CategoryName { set; get; }
        public string CategoryDescription { set; get; }
        public string BillerId { set; get; }
        public string BillerName { set; get; }
        public string CustomerField1 { get; set; }
        public string CustomerField2 { get; set; }
        public string SupportEmail { get; set; }
        public string PaydirectProductId { get; set; }
        public string PaydirectInstitutionId { get; set; }
        public string Narration { get; set; }
        public string ShortName { set; get; }
        public string Surcharge { set; get; }
        public string CurrencyCode { set; get; }
        public string QuickTellerSiteUrlName { get; set; }
        public string AmountType { get; set; }
        public string CurrencySymbol { get; set; }
        public string CustomSectionUrl { get; set; }
        public string LogoUrl { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string mediumImageId { get; set; }
        public string smallImageId { get; set; }
        public string largeImageId { get; set; }
    }
}
