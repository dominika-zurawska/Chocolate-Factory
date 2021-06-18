using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Entities
{
    internal class Contractor
    {

        #region Attributes

        public sbyte? Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public sbyte? IdAddress { get; set; }

        #endregion

        #region Constructors
        public Contractor()
        {
        }

        // creating an object based on MySqlDataReader
        public Contractor(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader[Properties.DBTablesNames.Contractors.Id].ToString());
            Name = reader[Properties.DBTablesNames.Contractors.Name].ToString();
            NIP = reader[Properties.DBTablesNames.Contractors.TaxIdentificationNumber].ToString();
            IdAddress = sbyte.Parse(reader[Properties.DBTablesNames.Contractors.Address].ToString());
        }

        // creating object not yet added to the database with id = null
        public Contractor(string name, string nip)
        {
            Id = null;
            Name = name.Trim();
            NIP = NIP.Trim();
            IdAddress = null;
        }

        public Contractor(Contractor contractor)
        {
            Id = contractor.Id;
            Name = contractor.Name;
            NIP = contractor.NIP;
            IdAddress = contractor.IdAddress;
        }

        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"('{Name}', '{NIP}')";
        }

        override public string ToString()
        {
            return $"{Name}";
        }

        public override bool Equals(object obj)
        {
            var contractor = obj as Contractor;
            if (contractor is null) return false;
            if (Name.ToLower() != contractor.Name.ToLower()) return false;
            if (NIP.ToLower() != contractor.NIP.ToLower()) return false;
            if (IdAddress != contractor.IdAddress) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
