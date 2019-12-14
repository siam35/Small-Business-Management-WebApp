using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature.Model.Model
{
    public class Customer
    {
        public Customer()
        {
            Sales = new List<Sales>();
        }
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public int LoyalityPoint { get; set; }

        public List<Sales> Sales { get; set; }
    }
}
