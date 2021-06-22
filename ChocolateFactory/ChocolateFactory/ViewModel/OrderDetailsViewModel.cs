using System;
using System.Collections.ObjectModel;

namespace ChocolateFactory.ViewModel
{
    using Model;
    using DAL.Entities;
    using BaseClass;
    using System.Windows.Input;

    class OrderDetailsViewModel : BaseViewModel
    {
        #region Private components

        private MainModel model = null;
        private OrderManager orderManager = null;

        private OrderManager _orderManager = new OrderManager();

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
        }

        public string date;
        public string Date
        {
            get { return date; }
            private set
            {
                date = value;
                onPropertyChanged(nameof(Date));
            }
        }


        public string contractorId;
        public string ContractorId
        {
            get { return contractorId; }
            private set
            {
                contractorId = value;
                onPropertyChanged(nameof(ContractorId));
            }
        }

        public string contractorName;
        public string ContractorName
        {
            get { return contractorId; }
            private set
            {
                contractorName = value;
                onPropertyChanged(nameof(ContractorName));
            }
        }

        public string nip;
        public string Nip
        {
            get { return nip; }
            private set
            {
                nip = value;
                onPropertyChanged(nameof(Nip));
            }
        }

        public string city;
        public string City
        {
            get { return city; }
            private set
            {
                city = value;
                onPropertyChanged(nameof(City));
            }
        }

        public string street;
        public string Street
        {
            get { return street; }
            private set
            {
                street = value;
                onPropertyChanged(nameof(Street));
            }
        }

        public string houseNumber;
        public string HouseNumber
        {
            get { return houseNumber; }
            private set
            {
                houseNumber = value;
                onPropertyChanged(nameof(HouseNumber));
            }
        }

        public string flatNumber;
        public string FlatNumber
        {
            get { return flatNumber; }
            private set
            {
                flatNumber = value;
                onPropertyChanged(nameof(FlatNumber));
            }
        }

        public string postalCode;
        public string PostalCode
        {
            get { return postalCode; }
            private set
            {
                postalCode = value;
                onPropertyChanged(nameof(PostalCode));
            }
        }

        public string amount;
        public string Amount
        {
            get { return amount; }
            private set
            {
                amount = value;
                onPropertyChanged(nameof(PostalCode));
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Commands

        private ICommand print;
        public ICommand Print
        {
            get
            {
                return print ?? (print = new RelayCommand(
                    (p) =>
                    {
                        orderManager.PrintOrder();
                    }
                    , p => true));
            }
        }




        #endregion
    }
}
