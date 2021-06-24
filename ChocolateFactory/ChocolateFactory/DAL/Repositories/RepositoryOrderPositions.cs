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
        private static string INSERT_POSITION = $"INSERT INTO `{Properties.DBTablesNames.OrderPositions.TableName}` " +
            $"(`{Properties.DBTablesNames.OrderPositions.Order}`, `{Properties.DBTablesNames.OrderPositions.Product}`, " +
            $"`{Properties.DBTablesNames.OrderPositions.Quantity}`) VALUES";

        #endregion

        #region CRUD
        public static ObservableCollection<OrderPosition> GetOrderPositions(int idOrder)
        {
            ObservableCollection<OrderPosition> orderPositions = new ObservableCollection<OrderPosition>();
            using (var connection = DBConnection.Instance.Connection)
            {
                string ALL_ORDERED_PRODUCTS = $"SELECT * FROM `{Properties.DBTablesNames.OrderPositions.TableName}` " +
                    $"WHERE `{Properties.DBTablesNames.OrderPositions.Order}`={idOrder}";

                MySqlCommand command = new MySqlCommand(ALL_ORDERED_PRODUCTS, connection);
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        orderPositions.Add(new OrderPosition(reader));
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return orderPositions;
        }

        public static bool InsertOrderPosition(OrderPosition orderPosition)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_POSITION} {orderPosition.ToInsert()}", connection);
                
                try
                {
                    connection.Open();
                    var id = command.ExecuteNonQuery();
                    state = true;
                    orderPosition.Id = (int)command.LastInsertedId;
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return state;
        }

        public static bool UpdateOrderedProduct(OrderPosition orderPosition)
        {
            return true;
        }

        public static bool DeleteOrderedProduct(int idOrder)
        {
            return true;
        }


        #endregion

    }
}
