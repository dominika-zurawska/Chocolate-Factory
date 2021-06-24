using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Repositories
{
    using Entities;

    static class RepositoryContractors
    {
        #region Queries
        private static string ALL_CONTRACTORS = $"SELECT * FROM `{Properties.DBTablesNames.Contractors.TableName}`";

        #endregion

        #region CRUD
        public static List<Contractor> GetAllContractors()
        {
            List<Contractor> contractors = new List<Contractor>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_CONTRACTORS, connection);
                try 
                { 
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        contractors.Add(new Contractor(reader));
                    connection.Close();
                }
                catch (Exception error) 
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return contractors;
        }

        public static Contractor GetContractor(int idContractor)
        {
            Contractor contractor = null;
            using (var connection = DBConnection.Instance.Connection)
            {
                string GET_CONTRACTOR = $"SELECT * FROM `{Properties.DBTablesNames.Contractors.TableName}` WHERE `{Properties.DBTablesNames.Contractors.Id}`={idContractor}";

                MySqlCommand command = new MySqlCommand(GET_CONTRACTOR, connection);
                
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        contractor = new Contractor(reader);
                    connection.Close();
                }
                catch (Exception error)
                {
                    Model.DbErrorNotifier.notifyError(error);
                }
            }
            return contractor;
        }



        public static bool InsertContractor(Address address)
        {
            return true;
        }

        public static bool UpdateContractor(Address address)
        {
            return true;
        }

        public static bool DeleteContractor(int idAddress)
        {
            return true;
        }


        #endregion

    }
}
