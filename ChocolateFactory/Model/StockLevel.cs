using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    public class StockLevel
    {
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public StockLevel(Product product, int productQuantity)
        {
            Product = product;
            ProductQuantity = productQuantity;
        }
    }
}
