using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;
    

    static class RepositoryOrderPositions
    {
        #region Queries
        private const string INSERT_POSITION = "INSERT INTO `order_positions` (`id_order`, `id_product`, `quantity`) VALUES";

        #endregion

        #region CRUD
        public static ObservableCollection<OrderPosition> GetOrderPositions(sbyte idOrder)
        {
            ObservableCollection<OrderPosition> orderPosition = new ObservableCollection<OrderPosition>();
            using (var connection = DBConnection.Instance.Connection)
            {
                string ALL_ORDERED_PRODUCTS = $"SELECT * FROM `order_positions` WHERE id_order={idOrder}";


                MySqlCommand command = new MySqlCommand(ALL_ORDERED_PRODUCTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    orderPosition.Add(new OrderPosition(reader));
                connection.Close();
            }
            return orderPosition;
        }

        public static bool InsertOrderPosition(OrderPosition orderPosition)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_POSITION} {orderPosition.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                orderPosition.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool UpdateOrderedProduct(OrderPosition orderPosition)
        {
            return true;
        }

        public static bool DeleteOrderedProduct(sbyte idOrder)
        {
            return true;
        }


        #endregion

    }
}
