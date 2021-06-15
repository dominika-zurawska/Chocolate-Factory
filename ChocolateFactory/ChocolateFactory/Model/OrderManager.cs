using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    public class OrderManager
    {
        public void AddPosition(Product product, int quantity)
        {
            var orderPosition = new OrderPosition(product, quantity);
            //dodanie do bazy danych
        }
        
        public void DeletePosition(Guid orderId)
        {
            // usunięcie z bazy danychna podstawie ID
        }

        public void EditPosition(Guid orderId)
        {
            //pobranie z bazy danych na podstawie ID
            
        }

        public void SubmitOrder(Contractor contractor, List<OrderPosition> orderPositions, decimal amount)
        {
            var order = new Order(contractor, orderPositions, amount);
            //zapis do bazy danych
        }

        public void ShowDetails(Guid orderId)
        {
            //pobranie z bazy danych na podstawie ID
        }

        public void RepeatOrder(Guid orderId)
        {
            //pobranie z bazy danych zamówieniea na podstawie ID
            //utworzenie z tego nowego zamówienia
        }
    }
}
