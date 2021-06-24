using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;

    static class RepositoryAddresses
    {
        #region Queries
        private static string ALL_ADDRESSES = $"SELECT * FROM `{Properties.DBTablesNames.Addresses.TableName}`";

        #endregion

        #region CRUD
        public static List<Address> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_ADDRESSES, connection);
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        addresses.Add(new Address(reader));
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return addresses;
        }

        public static Address GetAddress(int idAddress)
        {
            Address address = null;
            using (var connection = DBConnection.Instance.Connection)
            {
                string GET_ADDRESS = $"SELECT * FROM `{Properties.DBTablesNames.Addresses.TableName}` WHERE `{Properties.DBTablesNames.Addresses.Id}`={idAddress}";

                MySqlCommand command = new MySqlCommand(GET_ADDRESS, connection);
                
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        address = new Address(reader);
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return address;
        }



        public static bool InsertAddress(Address address)
        {
            return true;
        }

        public static bool UpdateAddress(Address address)
        {
            return true;
        }

        public static bool DeleteAddress(int idAddress)
        {
            return true;
        }


        #endregion

    }
}
