using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ChocolateFactory.DAL.Entities
{
    internal class Product
    {
        #region Attributes

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Constructors

        // creating an object based on MySqlDataReader
        public Product(MySqlDataReader reader) 
        { 
            Id = int.Parse(reader[Properties.DBTablesNames.Products.Id].ToString());
            Name = reader[Properties.DBTablesNames.Products.Name].ToString();
            Unit = reader[Properties.DBTablesNames.Products.Unit].ToString();
            PricePerUnit = decimal.Parse(reader[Properties.DBTablesNames.Products.PricePerUnit].ToString());
            Quantity = int.Parse(reader[Properties.DBTablesNames.Products.Quantity].ToString());
        }

        // creating object not yet added to the database with id = null
        public Product(string name, string unit, decimal ppu, int quantity)
        {
            Id = null;
            Name = name.Trim();
            Unit = unit.Trim();
            PricePerUnit = ppu;
            Quantity = quantity;
        }

        public Product(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Unit = product.Unit;
            PricePerUnit = product.PricePerUnit;
            Quantity = product.Quantity;
        }

        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"('{Name}', '{Unit}', {PricePerUnit}, {Quantity})";
        }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            if (product is null) return false;
            if (Name.ToLower() != product.Name.ToLower()) return false;
            if (Unit.ToLower() != product.Unit.ToLower()) return false;
            if (PricePerUnit != product.PricePerUnit) return false;
            //if (Quantity != product.Quantity) return false;
            return true;
        }

        override public string ToString()
        {
            return $"{Name}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
