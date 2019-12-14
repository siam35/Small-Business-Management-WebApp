using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.Model.Model;

namespace Signature.Models
{
    public class SupplierViewModel
    {
        public SupplierViewModel()
        {
            suppliers = new List<Supplier>();
            Purchaseses = new List<Purchases>();
        }
        public int Id { set; get; }

        [Required(ErrorMessage = "Code Cannot be empty")]
        [MaxLength(4, ErrorMessage = "Code should be of 4 character")]
        [MinLength(4, ErrorMessage = "Code should be of 4 character")]
        [Remote("IsCodeExist", "Supplier", AdditionalFields = "Id", ErrorMessage = "This Code already exists")]
        public string Code { set; get; }
        //  [Remote("IsNameAvailble", "Supplier", ErrorMessage = "Name Already Exist.")]
        
        [Required(ErrorMessage = "Name Cannot be empty")]
        public string Name { set; get; }
        [Index(IsUnique = true)]

        public string Address { set; get; }

        [Required(ErrorMessage = "Contact Cannot be empty")]
        [Remote("IsContactExist", "Supplier", AdditionalFields = "Id", ErrorMessage = "This Contact already exists")]
        [MaxLength(11, ErrorMessage = "Contact should be of 11 character")]
        [MinLength(11, ErrorMessage = "Contact should be of 11 character")]
        public string Contact { set; get; }

        [Required(ErrorMessage = "Email Cannot be empty")]
        [Remote("IsEmailExist", "Supplier", AdditionalFields = "Id", ErrorMessage = "This Email already exists")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { set; get; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { set; get; }
    

        public string Search { get; set; }

        public List<Supplier> suppliers { set; get; }

        public List<Purchases> Purchaseses { set; get; }
    }
}