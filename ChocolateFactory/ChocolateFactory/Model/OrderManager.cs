using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public class OrderManager
    {
        #region Private components

        private MainModel model = null;

        #endregion

        #region Constructors
        public OrderManager()
        {
        }

        internal OrderManager(MainModel model)
        {
            this.model = model;
        }
        #endregion

        internal void AddPosition(Product product, int quantity, ObservableCollection<Product> productsList)
        {
            Product newProduct = new Product(product);
            newProduct.Quantity = quantity;
            if (!productsList.Contains(newProduct))
            {
                productsList.Add(newProduct);
            }
        }

        internal void DeletePosition(ObservableCollection<Product> productsList, int productsListSelectedIndex)
        {
            productsList.RemoveAt(productsListSelectedIndex);
        }

        internal void EditPosition(ObservableCollection<Product> productsList, int productsListSelectedIndex, Product product, int quantity)
        {
            Product editedProduct = new Product(product);
            editedProduct.Quantity = quantity;
            productsList.RemoveAt(productsListSelectedIndex);
            productsList.Insert(productsListSelectedIndex, editedProduct);
        }

        internal void SubmitOrder(Contractor contractor, ObservableCollection<Product> productsList)
        {
            // count total amount
            decimal amount = (decimal)0.0;
            foreach (Product product in productsList)
            {
                decimal amountForProduct = (decimal)product.PricePerUnit * (decimal)product.Quantity;
                amount += amountForProduct;
            }

            // add new order to the database
            Order newOrder = new Order((int)contractor.Id, DateTime.Now, amount);
            if (RepositoryOrders.InsertOrder(newOrder))
            {
                newOrder.ContractorName= RepositoryContractors.GetContractor((int)contractor.Id).Name;
                model.Orders.Add(newOrder);
            }

            // add ordered products to table order_positions
            foreach (Product product in productsList) 
            {
                OrderPosition newOrderPosition = new OrderPosition((int)newOrder.Id, (int)product.Id, product.Quantity);
                RepositoryOrderPositions.InsertOrderPosition(newOrderPosition);
            }

            // reduce the stock level of ordered products
            foreach (Product product in productsList)
            {
                int newQuantity = model.Products[(int)product.Id-1].Quantity - product.Quantity;
                Product newProduct = new Product(product);
                newProduct.Quantity = newQuantity;
                RepositoryProducts.UpdateProductQuantity(newProduct);
            }

            // get new products data from database
            model.Products = null;
            model.Products = new ObservableCollection<Product>();
            var products = RepositoryProducts.GetAllProducts();
            foreach (var p in products)
                model.Products.Add(p);

        }

        internal void ShowDetails(int orderId, ref Order orderData, ref Contractor contractorData, ref Address addressData, ref ObservableCollection<Product> orderProductsData)
        {
            orderData = RepositoryOrders.GetOrder(orderId);
            contractorData = RepositoryContractors.GetContractor((int)orderData.IdContractor);
            addressData = RepositoryAddresses.GetAddress((int)contractorData.IdAddress);

            ObservableCollection<OrderPosition> orderPositions = RepositoryOrderPositions.GetOrderPositions(orderId);
            orderProductsData = new ObservableCollection<Product>(); // reset products list
            foreach (OrderPosition position in orderPositions)
            {
                Product product = RepositoryProducts.GetProduct(position.IdProduct);
                product.Quantity = position.Quantity;
                orderProductsData.Add(product);
            }
        }

        internal ObservableCollection<Product> GetPositions(int orderId)
        {
            ObservableCollection<OrderPosition> orderPositions = RepositoryOrderPositions.GetOrderPositions(orderId);
            ObservableCollection<Product> productsList = new ObservableCollection<Product>();
            foreach (OrderPosition position in orderPositions)
            {
                Product product = RepositoryProducts.GetProduct(position.IdProduct);

                product.Quantity = position.Quantity;
                productsList.Add(product);
            }
            return productsList;
        }

        internal void RepeatOrder(int orderId)
        {
            // get order by ID
            Order oldOrder = RepositoryOrders.GetOrder(orderId);

            Contractor contractor = RepositoryContractors.GetContractor(oldOrder.IdContractor);
            ObservableCollection<OrderPosition> orderPositions = RepositoryOrderPositions.GetOrderPositions(orderId);

            ObservableCollection<Product> productsList = new ObservableCollection<Product>();
            foreach (OrderPosition position in orderPositions) 
            {
                Product product = RepositoryProducts.GetProduct(position.IdProduct);

                product.Quantity = position.Quantity;
                productsList.Add(product);
            }

            // re-order
            SubmitOrder(contractor, productsList);
        }

        internal decimal CountAmount(ObservableCollection<Product> productsList) 
        {
            decimal amount = 0;
            foreach (Product p in productsList) {
                amount += (decimal)p.PricePerUnit * (decimal)p.Quantity;
            }
            return amount;
        }

    }
}
