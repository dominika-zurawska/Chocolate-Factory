using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChocolateFactory.DAL.Entities
{
    class OrderedProduct
    {

        #region Attributes

        public sbyte? Id { get; set; }
        public sbyte IdOrder { get; set; }
        public sbyte IdProduct { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Constructors

        // creating an object based on MySqlDataReader
        public OrderedProduct(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader[Properties.DBTablesNames.OrderedProducts.Id].ToString());
            IdOrder = sbyte.Parse(reader[Properties.DBTablesNames.OrderedProducts.Order].ToString());
            IdProduct = sbyte.Parse(reader[Properties.DBTablesNames.OrderedProducts.Product].ToString());
            Quantity = int.Parse(reader[Properties.DBTablesNames.OrderedProducts.Quantity].ToString());
        }

        // creating object not yet added to the database with id = null
        public OrderedProduct(sbyte idOrder, sbyte idProduct, int quantity)
        {
            Id = null;
            IdOrder = idOrder;
            IdProduct = idProduct;
            Quantity = quantity;
        }

        public OrderedProduct(OrderedProduct orderedProduct)
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
            var orderedProduct = obj as OrderedProduct;
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
