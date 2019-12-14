using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Model.Model
{
    public class Supplier
    {
        public Supplier()
        {
            Purchaseses = new List<Purchases>();
        }
        public int Id { set; get; }

        public string Code { set; get; }

        public string Name { set; get; }

        public string Address { set; get; }

        public string Contact { set; get; }

        public string Email { set; get; }

        public string ContactPerson { set; get; }

        public List<Purchases> Purchaseses { set; get; }


    }
}
