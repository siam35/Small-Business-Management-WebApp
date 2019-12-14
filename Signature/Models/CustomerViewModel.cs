using Signature.Model.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Signature.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            Sales = new List<Sales>();
            Customers = new List<Customer>();

        }

        public int Id { set; get; }

        [Required(ErrorMessage = "Code Cannot be empty")]
        [Remote("IsCodeExist", "Customer", AdditionalFields = "Id", ErrorMessage = "This Code already exists")]
        [MaxLength(4, ErrorMessage = "Code should be of 4 character")]
        [MinLength(4, ErrorMessage = "Code should be of 4 character")]
        public string Code { set; get; }

        [Display(Name = "Customer Name: ")]
        [Required(ErrorMessage = "Name Cannot be empty")]
        public string Name { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Contact Cannot be empty")]
       [Remote("IsContactExist", "Customer", AdditionalFields = "Id", ErrorMessage = "This Contact already exists")]
        [MaxLength(11, ErrorMessage = "Contact should be of 11 character")]
        [MinLength(11, ErrorMessage = "Contact should be of 11 character")]
        public string Contact { set; get; }

        [Required(ErrorMessage = "Email Cannot be empty")]
        [Remote("IsEmailExist", "Customer", AdditionalFields = "Id", ErrorMessage = "This Email already exists")]
    //    [Remote("IsEmailExist", "Category", AdditionalFields = "Id", ErrorMessage = "This Email already exists")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { set; get; }

        [Display(Name = "Loyality Point: ")]
        [Range(0,99, ErrorMessage = "LoyalityPoint Range is between 0 to 99")]
        public int LoyalityPoint { get; set; }

        public List<Customer> Customers { set; get; }

        public List<Sales> Sales { get; set; }

    }
}