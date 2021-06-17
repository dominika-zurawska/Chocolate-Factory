using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Entities
{
    using Repositories;
    class Order
    {

        #region Attributes

        public sbyte? Id { get; set; }
        public sbyte IdContractor { get; set; }
        public DateTime OrderDate { get; set; }
        public string Notes { get; set; }
        public string ContractorName { get; set; }

        #endregion

        #region Constructors

        // creating an object based on MySqlDataReader
        public Order(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader[Properties.DBTablesNames.Orders.Id].ToString());
            IdContractor = sbyte.Parse(reader[Properties.DBTablesNames.Orders.Contractor].ToString());
            OrderDate = DateTime.Parse(reader[Properties.DBTablesNames.Orders.OrderDate].ToString());
            Notes = reader[Properties.DBTablesNames.Orders.Notes].ToString();
            ContractorName = reader[Properties.DBTablesNames.Contractors.Name].ToString();
        }

        // creating object not yet added to the database with id = null
        public Order(sbyte idContractor, DateTime orderDate, string notes = "")
        {
            Id = null;
            IdContractor = idContractor;
            OrderDate = orderDate;
            Notes = notes.Trim();
        }

        public Order(Order order)
        {
            Id = order.Id;
            IdContractor = order.IdContractor;
            OrderDate = order.OrderDate;
            Notes = order.Notes;
        }

        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"({IdContractor}, {OrderDate}, '{Notes}')";
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            if (order is null) return false;
            if (!OrderDate.Equals(order.OrderDate)) return false;
            if (Notes.ToLower() != order.Notes.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
