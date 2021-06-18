using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.ViewModel
{
    using Model;
    using DAL.Entities;
    using BaseClass;
    using System.Windows.Input;

    class OrderDetailsViewModel:BaseViewModel
    {
        #region Private components

        private MainModel model = null;
        private OrderManager orderManager = null;

        private sbyte orderId;

        // test ShowDetails
        private Order orderData;
        private Contractor contractorData;
        private Address addressData;
        private ObservableCollection<Product> orderProductsData = null;
        //
        #endregion

        #region Properties
        // test ShowDetails
        public Order OrderData
        {
            get { return orderData; }
            set
            {
                orderData = value;
                onPropertyChanged(nameof(OrderData));
            }
        }

        public Contractor ContractorData
        {
            get { return contractorData; }
            set
            {
                contractorData = value;
                onPropertyChanged(nameof(ContractorData));
            }
        }

        public Address AddressData
        {
            get { return addressData; }
            set
            {
                addressData = value;
                onPropertyChanged(nameof(AddressData));
            }
        }

        public ObservableCollection<Product> OrderProductsData
        {
            get { return orderProductsData; }
            set
            {
                orderProductsData = value;
                onPropertyChanged(nameof(OrderProductsData));
            }
        }
        //
        #endregion

        #region Constructors
        public OrderDetailsViewModel(MainModel model, OrderManager orderManager, sbyte orderId)
        {
            this.model = model;
            this.orderId = orderId;
            this.orderManager = orderManager;

            // test ShowDetails
            orderManager.ShowDetails(orderId, ref orderData, ref contractorData, ref addressData, ref orderProductsData);
            Console.WriteLine("Nr zamówienia " + orderId.ToString());
            Console.WriteLine(OrderData.ToInsert());
            Console.WriteLine(ContractorData.ToInsert());
            Console.WriteLine(AddressData.ToInsert());
            foreach (Product p in orderProductsData) { Console.WriteLine(p.ToInsert()); }
            //

        }
        #endregion

        #region Properties

        #endregion

        #region Commands

        #endregion
    }
}
