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
        public decimal Amount { get; set; }
        public string ContractorName { get; set; }

        #endregion

        #region Constructors
        public Order() 
        {
        }

        // creating an object based on MySqlDataReader
        public Order(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader[Properties.DBTablesNames.Orders.Id].ToString());
            IdContractor = sbyte.Parse(reader[Properties.DBTablesNames.Orders.Contractor].ToString());
            OrderDate = DateTime.Parse(reader[Properties.DBTablesNames.Orders.OrderDate].ToString());
            Amount = decimal.Parse(reader[Properties.DBTablesNames.Orders.Amount].ToString());
            ContractorName = reader[Properties.DBTablesNames.Contractors.Name].ToString();
        }

        // creating object not yet added to the database with id = null
        public Order(sbyte idContractor, DateTime orderDate, decimal amount)
        {
            Id = null;
            IdContractor = idContractor;
            OrderDate = orderDate;
            Amount = amount;
        }

        public Order(Order order)
        {
            Id = order.Id;
            IdContractor = order.IdContractor;
            OrderDate = order.OrderDate;
            Amount = order.Amount;
        }

        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"({IdContractor}, \"{OrderDate.ToString("yyyy-MM-dd HH:mm:ss")}\", {Amount.ToString().Replace(",",".")})";
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            if (order is null) return false;
            if (!OrderDate.Equals(order.OrderDate)) return false;
            if (!Amount.Equals(order.Amount)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
