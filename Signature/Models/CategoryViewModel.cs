using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.Model.Model;

namespace Signature.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Categories = new List<Category>();
        }
       
        public int Id { set; get; }
        public string initialCategorycode { get; set; }

        [Required(ErrorMessage = "Code Cannot be empty")]
       [Remote("IsCodeExist", "Category", AdditionalFields = "Id", ErrorMessage = "This Code already exists")]
        [MaxLength(4, ErrorMessage = "Code should be of 4 character")]
        [MinLength(4, ErrorMessage = "Code should be of 4 character")]
        public string Code { set; get; }

        [Required(ErrorMessage = "Name Cannot be empty")]
        [Remote("IsNameExist", "Category", AdditionalFields = "Id", ErrorMessage = "This Name already exists")]
        public string Name { set; get; }
        
        public List<Category> Categories { set; get; }

    }
}