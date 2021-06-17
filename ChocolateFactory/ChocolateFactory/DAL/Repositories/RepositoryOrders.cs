using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;

    static class RepositoryOrders
    {
        #region Queries
        private const string ALL_ORDERS = "select orders.id, id_contractor, order_date, notes, amount, name from orders join contractors c on c.id = id_contractor order by 1;";
        private const string INSERT_ORDER = "INSERT INTO `orders`(`id_contractor`, `order_date`, `notes`) VALUES";

        #endregion

        #region CRUD
        public static List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_ORDERS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    orders.Add(new Order(reader));
                connection.Close();
            }
            return orders;
        }

        public static bool InsertOrder(Order order)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_ORDER} {order.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                order.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool UpdateOrder(Order order)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string UPDATE_ORDER = $"UPDATE orders SET {Properties.DBTablesNames.Orders.Contractor}='{order.IdContractor}', " +
                    $"{Properties.DBTablesNames.Orders.OrderDate}='{order.OrderDate}', " +
                    $"{Properties.DBTablesNames.Orders.Notes}={order.Notes}', {Properties.DBTablesNames.Orders.Amount}={order.Amount} WHERE id_o={order.Id}";

                MySqlCommand command = new MySqlCommand(UPDATE_ORDER, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }

        public static bool DeleteOrder(sbyte idOrder)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_ORDER = $"DELETE FROM orders WHERE id={idOrder}";

                MySqlCommand command = new MySqlCommand(DELETE_ORDER, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }


        #endregion

    }
}
