using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.Model.Model;

namespace Signature.Models
{
    public class StockViewModel
    {
        public StockViewModel()
        {
            PurchasesDetailses = new List<PurchasesDetails>();
            SalesDetailses = new List<SalesDetails>();
            CategorySelectListItems = new List<SelectListItem>();
            StockView = new List<StockViewModel>();
        }
        public string Code { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime ExpireDate { get; set; }
        public int ExpireQuantity { get; set; }
        public int OpeningBalance { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public int ClosingBalance { get; set; }

        public string Name { get; set; }
        public int ProductId { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<PurchasesDetails> PurchasesDetailses { get; set; }
        public List<SalesDetails> SalesDetailses { get; set; }
        public List<StockViewModel> StockView { get; set; }

        public List<SelectListItem> CategorySelectListItems { get; set; }
    }
}