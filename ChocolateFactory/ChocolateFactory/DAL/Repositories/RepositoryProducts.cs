using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;

    static class RepositoryProducts
    {
        #region Queries
        private const string ALL_PRODUCTS = "SELECT * FROM products";

        #endregion

        #region CRUD
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_PRODUCTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    products.Add(new Product(reader));
                connection.Close();
            }
            return products;
        }

        public static bool InsertProduct(Address address)
        {
            return true;
        }

        public static bool UpdateProduct(Address address)
        {
            return true;
        }

        public static bool DeleteProduct(sbyte idAddress)
        {
            return true;
        }


        #endregion

    }
}
