using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Entities
{
    class OrderPosition
    {

        #region Attributes

        public sbyte? Id { get; set; }
        public sbyte IdOrder { get; set; }
        public sbyte IdProduct { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Constructors

        // creating an object based on MySqlDataReader
        public OrderPosition(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader[Properties.DBTablesNames.OrderPositions.Id].ToString());
            IdOrder = sbyte.Parse(reader[Properties.DBTablesNames.OrderPositions.Order].ToString());
            IdProduct = sbyte.Parse(reader[Properties.DBTablesNames.OrderPositions.Product].ToString());
            Quantity = int.Parse(reader[Properties.DBTablesNames.OrderPositions.Quantity].ToString());
        }

        // creating object not yet added to the database with id = null
        public OrderPosition(sbyte idOrder, sbyte idProduct, int quantity)
        {
            Id = null;
            IdOrder = idOrder;
            IdProduct = idProduct;
            Quantity = quantity;
        }

        public OrderPosition(OrderPosition orderedProduct)
        {
            Id = orderedProduct.Id;
            IdOrder = orderedProduct.IdOrder;
            IdProduct = orderedProduct.IdProduct;
            Quantity = orderedProduct.Quantity;
        }

        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"({IdOrder}, {IdProduct}, {Quantity})";
        }

        public override bool Equals(object obj)
        {
            var orderedProduct = obj as OrderPosition;
            if (orderedProduct is null) return false;
            if (Quantity != orderedProduct.Quantity) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
