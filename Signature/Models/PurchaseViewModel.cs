using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.Model.Model;

namespace Signature.Models
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel()
        {
            PurchasesDetailses = new List<PurchasesDetails>();
        }

        public int Id { get; set; }
        
        public DateTime Date { set; get; }

        public string Bill { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int ProductsId { get; set; }

        public Product Product { get; set; }

        public int PurchaseId { get; set; }

        public Purchases Purchases { get; set; }

        public DateTime ManufacturedDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double MRP { get; set; }

        public string Remarks { get; set; }

        public DateTime StartDateTime { set; get; }
        public DateTime EndDateTime { set; get; }

        public List<Purchases> Purchaseses { get; set; }
        public List<PurchasesDetails> PurchasesDetailses { get; set; }
        public List<SelectListItem> SupplierSelectListItems { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
    }
}