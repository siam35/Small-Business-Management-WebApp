using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Model.Model
{
    public class PurchasesDetails
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime ManufacturedDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double MRP { get; set; }

        public string Remarks { get; set; }

        public int PurchasesId { get; set; }

        public Purchases Purchases { get; set; }
    }
}
