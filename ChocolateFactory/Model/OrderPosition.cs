using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    public class OrderPosition
    {
        public Guid OrderPositionId { get; set; }
        public Product Product {get; set;}
        public int Quantity { get; set; }

        public OrderPosition(Product product, int quantity)
        {
            OrderPositionId = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
        }
    }
}
