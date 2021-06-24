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
        private static string ALL_ORDERS = $"SELECT o.`{Properties.DBTablesNames.Orders.Id}`, `{Properties.DBTablesNames.Orders.Contractor}`," +
            $"`{Properties.DBTablesNames.Orders.OrderDate}`, `{Properties.DBTablesNames.Orders.Amount}`, `{Properties.DBTablesNames.Contractors.Name}` " +
            $"FROM `{Properties.DBTablesNames.Orders.TableName}` o JOIN `{Properties.DBTablesNames.Contractors.TableName}` c ON " +
            $"c.`{Properties.DBTablesNames.Contractors.Id}` = `{Properties.DBTablesNames.Orders.Contractor}` ORDER BY 1;";

        private static string INSERT_ORDER = $"INSERT INTO `{Properties.DBTablesNames.Orders.TableName}` (`{Properties.DBTablesNames.Orders.Contractor}`, " +
            $"`{Properties.DBTablesNames.Orders.OrderDate}`, `{Properties.DBTablesNames.Orders.Amount}`) VALUES";

        #endregion

        #region CRUD
        public static List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_ORDERS, connection);
                
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        orders.Add(new Order(reader));
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return orders;
        }

        public static Order GetOrder(int idOrder)
        {
            Order order = null;
            using (var connection = DBConnection.Instance.Connection)
            {
                string GET_ORDER = $"SELECT o.`{Properties.DBTablesNames.Orders.Id}`, `{Properties.DBTablesNames.Orders.Contractor}`," +
                    $"`{Properties.DBTablesNames.Orders.OrderDate}`, `{Properties.DBTablesNames.Orders.Amount}`, `{Properties.DBTablesNames.Contractors.Name}` " +
                    $"FROM `{Properties.DBTablesNames.Orders.TableName}` o JOIN `{Properties.DBTablesNames.Contractors.TableName}` c ON " +
                    $"c.`{Properties.DBTablesNames.Contractors.Id}` = `{Properties.DBTablesNames.Orders.Contractor}` WHERE o.`{Properties.DBTablesNames.Orders.Id}`={idOrder};";
                MySqlCommand command = new MySqlCommand(GET_ORDER, connection);
                
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        order = new Order(reader);
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return order;
        }

        public static bool InsertOrder(Order order)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_ORDER} {order.ToInsert()}", connection);
                
                try
                {
                    connection.Open();
                    Console.WriteLine(order.ToInsert());
                    var id = command.ExecuteNonQuery();
                    state = true;
                    order.Id = (int)command.LastInsertedId;
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return state;
        }

        public static bool UpdateOrder(Order order)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string UPDATE_ORDER = $"UPDATE `{Properties.DBTablesNames.Orders.TableName}` SET `{Properties.DBTablesNames.Orders.Contractor}`={order.IdContractor}, " +
                    $"`{Properties.DBTablesNames.Orders.OrderDate}`={order.OrderDate}, " +
                    $"`{Properties.DBTablesNames.Orders.Amount}`={order.Amount} WHERE `{Properties.DBTablesNames.Orders.Id}`={order.Id}";

                MySqlCommand command = new MySqlCommand(UPDATE_ORDER, connection);
                
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

        public static bool DeleteOrder(int idOrder)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_ORDER = $"DELETE FROM `{Properties.DBTablesNames.Orders.TableName}` WHERE `{Properties.DBTablesNames.Orders.Id}`={idOrder}";

                MySqlCommand command = new MySqlCommand(DELETE_ORDER, connection);
                
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


        #endregion

    }
}
