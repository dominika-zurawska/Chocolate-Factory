using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;

    static class RepositoryOrderPositions
    {
        #region Queries

        #endregion

        #region CRUD
        public static List<OrderPosition> GetOrderedProducts(sbyte idOrder)
        {
            List<OrderPosition> orderedProducts = new List<OrderPosition>();
            using (var connection = DBConnection.Instance.Connection)
            {
                string ALL_ORDERED_PRODUCTS = $"SELECT * FROM ordered_products WHERE id_order={idOrder}";

                MySqlCommand command = new MySqlCommand(ALL_ORDERED_PRODUCTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    orderedProducts.Add(new OrderPosition(reader));
                connection.Close();
            }
            return orderedProducts;
        }

        public static bool InsertOrderedProduct(Order order)
        {
            return true;
        }

        public static bool UpdateOrderedProduct(Order order)
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
