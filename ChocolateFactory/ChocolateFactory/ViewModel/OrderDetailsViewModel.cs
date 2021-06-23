using System;
using System.Collections.ObjectModel;

namespace ChocolateFactory.ViewModel
{
    using Model;
    using DAL.Entities;
    using BaseClass;
    using System.Windows.Input;
    using System.Linq;

    class OrderDetailsViewModel : BaseViewModel
    {
        #region Private components

        private OrderManager _orderManager = null;
        private sbyte? addressId;

        // test ShowDetails
        private Order orderData;
        private Contractor contractorData;
        private Address addressData;
        private ObservableCollection<Product> orderProductsData;
        
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
            _orderManager = orderManager;
            contractorId = model.Orders.FirstOrDefault(o => o.Id == orderId).IdContractor;
            nip = model.Contractors.FirstOrDefault(c => c.Id == contractorId).NIP;
            contractorName = model.Contractors.FirstOrDefault(c => c.Id == contractorId).Name;
            addressId = model.Contractors.FirstOrDefault(c => c.Id == contractorId).IdAddress;
            city = model.Addresses.FirstOrDefault(a => a.Id == addressId).City;
            street = model.Addresses.FirstOrDefault(a => a.Id == addressId).City;
            houseNumber = model.Addresses.FirstOrDefault(a => a.Id == addressId).HouseNumber;
            flatNumber = model.Addresses.FirstOrDefault(a => a.Id == addressId).FlatNumber;
            postalCode = model.Addresses.FirstOrDefault(a => a.Id == addressId).PostalCode;
            date = model.Orders.FirstOrDefault(o => o.Id == orderId).OrderDate;
            amount = model.Orders.FirstOrDefault(o => o.Id == orderId).Amount;
            orderProductsData = orderManager.GetPositions(orderId);
        }

        public DateTime date;
        public DateTime Date
        {
            get { return date; }
            private set
            {
                date = value;
                onPropertyChanged(nameof(Date));
            }
        }


        public sbyte contractorId;
        public sbyte ContractorId
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
            get { return contractorName; }
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

        public decimal amount;
        public decimal Amount
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
                        _orderManager.PrintOrder();
                    }
                    , p => true));
            }
        }
        #endregion
    }
}
