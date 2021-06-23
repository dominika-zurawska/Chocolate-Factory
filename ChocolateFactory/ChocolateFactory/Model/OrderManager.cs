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
            product.Quantity = quantity;
            if (!productsList.Contains(product))
            {
                productsList.Add(product);
            }
        }

        internal void DeletePosition(ObservableCollection<Product> productsList, int productsListSelectedIndex)
        {
            productsList.RemoveAt(productsListSelectedIndex);
        }

        internal Product EditPosition(ObservableCollection<Product> productsList, int productsListSelectedIndex, Product product, int quantity)
        {
            Product editedProduct = new Product(product);
            editedProduct.Quantity = quantity;
            
            return editedProduct;
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
            Order newOrder = new Order((sbyte)contractor.Id, DateTime.Now, amount);
            if (RepositoryOrders.InsertOrder(newOrder))
            {
                newOrder.ContractorName= RepositoryContractors.GetContractor((sbyte)contractor.Id).Name;
                model.Orders.Add(newOrder);
            }

            // add ordered products to table order_positions
            foreach (Product product in productsList) 
            {
                OrderPosition newOrderPosition = new OrderPosition((sbyte)newOrder.Id, (sbyte)product.Id, product.Quantity);
                RepositoryOrderPositions.InsertOrderPosition(newOrderPosition);
            }
        }

        internal void ShowDetails(sbyte orderId, ref Order orderData, ref Contractor contractorData, ref Address addressData, ref ObservableCollection<Product> orderProductsData)
        {
            orderData = RepositoryOrders.GetOrder(orderId);
            contractorData = RepositoryContractors.GetContractor((sbyte)orderData.IdContractor);
            addressData = RepositoryAddresses.GetAddress((sbyte)contractorData.IdAddress);

            ObservableCollection<OrderPosition> orderPositions = RepositoryOrderPositions.GetOrderPositions(orderId);
            orderProductsData = new ObservableCollection<Product>(); // reset products list
            foreach (OrderPosition position in orderPositions)
            {
                Product product = RepositoryProducts.GetProduct(position.IdProduct);
                product.Quantity = position.Quantity;
                orderProductsData.Add(product);
            }
        }

        internal ObservableCollection<Product> GetPositions(sbyte orderId)
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

        internal void RepeatOrder(sbyte orderId)
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
    }
}
