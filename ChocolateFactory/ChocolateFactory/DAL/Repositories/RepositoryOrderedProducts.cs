using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;

    static class RepositoryOrderedProducts
    {
        #region Queries

        #endregion

        #region CRUD
        public static List<OrderedProduct> GetOrderedProducts(sbyte idOrder)
        {
            List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
            using (var connection = DBConnection.Instance.Connection)
            {
                string ALL_ORDERED_PRODUCTS = $"SELECT * FROM ordered_products WHERE id_order={idOrder}";

                MySqlCommand command = new MySqlCommand(ALL_ORDERED_PRODUCTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    orderedProducts.Add(new OrderedProduct(reader));
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
