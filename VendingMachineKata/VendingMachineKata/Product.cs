using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineKata
{
    public class Product
    {
        private string _name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            _name = name;
            Price = price;
        }
    }
}
