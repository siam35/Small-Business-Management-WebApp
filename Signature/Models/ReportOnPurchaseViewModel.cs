using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Signature.Model.Model;

namespace Signature.Models
{
    public class ReportOnPurchaseViewModel
    {
        public ReportOnPurchaseViewModel()
        {
            PurchasesDetailses = new List<PurchasesDetails>();
            SalesDetailses = new List<SalesDetails>();
            ReportOnPurchaseViewModels = new List<ReportOnPurchaseViewModel>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int AvailableQuantity { get; set; }
        public double CP { get; set; }
        public double MRP { get; set; }
        public double Profit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProductId { get; set; }

        public List<PurchasesDetails> PurchasesDetailses { get; set; }
        public List<SalesDetails> SalesDetailses { get; set; }
        public List<ReportOnPurchaseViewModel> ReportOnPurchaseViewModels { get; set; }
    }
}