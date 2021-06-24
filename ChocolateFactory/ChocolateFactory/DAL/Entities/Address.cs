using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Entities
{
    internal class Address
    {
        #region Attributes

        public int? Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PostalCode { get; set; }


        #endregion

        #region Constructors
        public Address() 
        {
        }

        // creating an object based on MySqlDataReader
        public Address(MySqlDataReader reader)
        {
            Id = int.Parse(reader[Properties.DBTablesNames.Addresses.Id].ToString());
            City = reader[Properties.DBTablesNames.Addresses.City].ToString();
            Street = reader[Properties.DBTablesNames.Addresses.Street].ToString();
            HouseNumber = reader[Properties.DBTablesNames.Addresses.HouseNumber].ToString();
            FlatNumber = reader[Properties.DBTablesNames.Addresses.FlatNumber].ToString();
            PostalCode = reader[Properties.DBTablesNames.Addresses.PostalCode].ToString();
        }

        // creating object not yet added to the database with id = null
        public Address(string name, string street, string houseNumber, string flatNumber, string postalCode)
        {
            Id = null;
            City = name.Trim();
            Street = street.Trim();
            HouseNumber = houseNumber.Trim();
            FlatNumber = flatNumber.Trim();
            PostalCode = postalCode.Trim();
        }

        public Address(Address address)
        {
            Id = address.Id;
            City = address.City;
            Street = address.Street;
            HouseNumber = address.HouseNumber;
            FlatNumber = address.FlatNumber;
            PostalCode = address.PostalCode;
        }

        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"('{City}', '{Street}', '{HouseNumber}', '{FlatNumber}', '{PostalCode}')";
        }

        public override bool Equals(object obj)
        {
            var address = obj as Address;
            if (address is null) return false;
            if (City.ToLower() != address.City.ToLower()) return false;
            if (Street.ToLower() != address.Street.ToLower()) return false;
            if (HouseNumber.ToLower() != address.HouseNumber.ToLower()) return false;
            if (FlatNumber.ToLower() != address.FlatNumber.ToLower()) return false;
            if (PostalCode.ToLower() != address.PostalCode.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
