using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.Model.Model;

namespace Signature.Models
{
    public class SalesViewModel
    {
        //public double GrandTotal { get; set; }

        public SalesViewModel()
        {
            SalesDetailses = new List<SalesDetails>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public DateTime Date { set; get; }
        
        public DateTime StartDateTime { set; get; }
        public DateTime EndDateTime { set; get; }
        
        public List<SalesDetails> SalesDetailses { get; set; }
        public List<SelectListItem> CustomerSelectListItems { get; set;}
        public  List<SelectListItem> ProductSelectListItems { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
        public List<Sales> Salses { get; set; }

        public int Quantity { get; set; }
        public double MRP { get; set; }
        public int SalesId { get; set; }
        public Sales Sales { get; set; }
    }
}