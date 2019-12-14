using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Signature.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Signature.Models
{
    public class ProductViewModel
    {

        public ProductViewModel()
        {
            Products = new List<Product>();
            SalesDetails = new List<SalesDetails>();
            PurchasesDetails =  new List<PurchasesDetails>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Code cannot be empty !!!")]
        [Remote("IsCodeExist", "Product", AdditionalFields = "Id", ErrorMessage = "This Code already exists")]
        [Display(Name = "Product Code: ")]
        [MaxLength(4, ErrorMessage = "Maximum code length is 4 !!!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name cannot be empty !!!")]
        [Remote("IsNameExist", "Product", AdditionalFields = "Id", ErrorMessage = "This Name already exists")]
        [Display(Name = "Product Name: ")]
        public string Name { get; set; }

        [Range(0,999, ErrorMessage = "ReorderLevel Range is between 0 to 999")]
        [Required(ErrorMessage = "ReorderLevel cannot be empty !!!")]
        [Display(Name = "Re-order Level: ")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "Description cannot be empty !!!")]
        [Display(Name = "Description: ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select a Category !!!")]
        [Display(Name = "Category: ")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Product> Products { get; set; }

        public List<SelectListItem> CategorySelectListItems { get; set; }

        public List<PurchasesDetails> PurchasesDetails { set; get; }

        public List<SalesDetails> SalesDetails { set; get; }
    }
}