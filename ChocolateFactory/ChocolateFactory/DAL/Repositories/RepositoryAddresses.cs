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
        private const string ALL_ADDRESSES = "SELECT * FROM addresses";

        #endregion

        #region CRUD
        public static List<Address> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_ADDRESSES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    addresses.Add(new Address(reader));
                connection.Close();
            }
            return addresses;
        }

        public static Address GetAddress(sbyte idAddress)
        {
            Address address = null;
            using (var connection = DBConnection.Instance.Connection)
            {
                string GET_ADDRESS = $"SELECT * FROM addresses WHERE id={idAddress}";

                MySqlCommand command = new MySqlCommand(GET_ADDRESS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                address = new Address(reader);
                connection.Close();
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

        public static bool DeleteAddress(sbyte idAddress)
        {
            return true;
        }


        #endregion

    }
}
