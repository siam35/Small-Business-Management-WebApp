using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace Signature.Model.Model
{
    public class Sales
    {

        public Sales()
        {
            SalesDetailses = new List<SalesDetails>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
     
        public DateTime Date { set; get; }
        public List<SalesDetails> SalesDetailses { get; set; }
    }
}
