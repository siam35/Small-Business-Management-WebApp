using System;
using System.Collections.Generic;


using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Model.Model
{
    public class Purchases
    {
        public Purchases()
        {
            PurchasesDetailses = new List<PurchasesDetails>();
        }

        public int Id { get; set; }
        public DateTime Date { set; get; }
        public string Bill { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public List<PurchasesDetails> PurchasesDetailses { get; set; }
    }
}
