using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Model.Model
{
    public class Product
    {
        public Product()
        {
            PurchasesDetails = new List<PurchasesDetails>();
            SalesDetails = new List<SalesDetails>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int ReorderLevel { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public List<PurchasesDetails> PurchasesDetails { set; get; }

        public List<SalesDetails> SalesDetails { set; get; }
    }
}
