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
        private static string ALL_PRODUCTS = $"SELECT * FROM `{Properties.DBTablesNames.Products.TableName}`";

        #endregion

        #region CRUD
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_PRODUCTS, connection);
                
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        products.Add(new Product(reader));
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return products;
        }

        public static Product GetProduct(int idProduct)
        {
            Product product = null;
            using (var connection = DBConnection.Instance.Connection)
            {
                string GET_PRODUCT = $"SELECT * FROM `{Properties.DBTablesNames.Products.TableName}` WHERE `{Properties.DBTablesNames.Products.Id}`={idProduct}";

                MySqlCommand command = new MySqlCommand(GET_PRODUCT, connection);
                
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        product = new Product(reader);
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return product;
        }

        public static bool InsertProduct(Product product)
        {
            return true;
        }

        public static bool UpdateProduct(Product product)
        {
            return true;
        }

        public static bool UpdateProductQuantity(Product product)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string UPDATE_PRODUCT = $"UPDATE `{Properties.DBTablesNames.Products.TableName}` SET `{Properties.DBTablesNames.Products.Quantity}`={product.Quantity}" +
                    $" WHERE `{Properties.DBTablesNames.Products.Id}`={product.Id}";

                MySqlCommand command = new MySqlCommand(UPDATE_PRODUCT, connection);

                try
                {
                    connection.Open();
                    var n = command.ExecuteNonQuery();
                    if (n == 1) state = true;

                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return state;
        }

        public static bool DeleteProduct(int idProduct)
        {
            return true;
        }


        #endregion

    }
}
