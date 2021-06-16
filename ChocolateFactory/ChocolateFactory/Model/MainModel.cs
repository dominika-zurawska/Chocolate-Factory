using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.ObjectModel;

    class MainModel
    {
        // database instance
        public ObservableCollection<Contractor> Contractors { get; set; } = new ObservableCollection<Contractor>();
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();


        public MainModel()
        {
            //getting data from database
            var contractors = RepositoryContractors.GetAllContractors();
            foreach (var c in contractors)
                Contractors.Add(c);
            var orders = RepositoryOrders.GetAllOrders();
            foreach (var o in orders)
                Orders.Add(o);
            var products = RepositoryProducts.GetAllProducts();
            foreach (var p in products)
                Products.Add(p);
        }

    }


}
