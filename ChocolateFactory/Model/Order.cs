using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Contractor Contractor { get; set; }
        public List<OrderPosition> OrderPositions { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(Contractor contrator, List<OrderPosition> orderPositions, decimal amount)
        {
            OrderId = Guid.NewGuid();
            Contractor = contrator;
            OrderPositions = orderPositions;
            Amount = amount;
            OrderDate = DateTime.Now;
        }
    }
}
