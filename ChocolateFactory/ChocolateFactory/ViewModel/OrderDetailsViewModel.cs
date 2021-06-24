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
        private Printer printer = new Printer();

        private Order orderData;
        private Contractor contractorData;
        private Address addressData;
        private ObservableCollection<Product> orderProductsData;
        private DateTime date;
        private int contractorId;
        private string contractorName;
        private string nip;
        private string city;
        private string street;
        private string houseNumber;
        private string flatNumber;
        private string postalCode;
        private decimal amount;

        #endregion

        #region Properties
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

        public DateTime Date
        {
            get { return date; }
            private set
            {
                date = value;
                onPropertyChanged(nameof(Date));
            }
        }

        public int ContractorId
        {
            get { return contractorId; }
            private set
            {
                contractorId = value;
                onPropertyChanged(nameof(ContractorId));
            }
        }

        public string ContractorName
        {
            get { return contractorName; }
            private set
            {
                contractorName = value;
                onPropertyChanged(nameof(ContractorName));
            }
        }

        public string Nip
        {
            get { return nip; }
            private set
            {
                nip = value;
                onPropertyChanged(nameof(Nip));
            }
        }

        public string City
        {
            get { return city; }
            private set
            {
                city = value;
                onPropertyChanged(nameof(City));
            }
        }

        public string Street
        {
            get { return street; }
            private set
            {
                street = value;
                onPropertyChanged(nameof(Street));
            }
        }

        public string HouseNumber
        {
            get { return houseNumber; }
            private set
            {
                houseNumber = value;
                onPropertyChanged(nameof(HouseNumber));
            }
        }

        public string FlatNumber
        {
            get { return flatNumber; }
            private set
            {
                flatNumber = value;
                onPropertyChanged(nameof(FlatNumber));
            }
        }

        public string PostalCode
        {
            get { return postalCode; }
            private set
            {
                postalCode = value;
                onPropertyChanged(nameof(PostalCode));
            }
        }

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


        #region Constructors

        public OrderDetailsViewModel(MainModel model, OrderManager orderManager, int orderId)
        {
            this._orderManager = orderManager;

            orderManager.ShowDetails(orderId, ref orderData, ref contractorData, ref addressData, ref orderProductsData);

            ContractorId = (int)ContractorData.Id;
            Nip = ContractorData.NIP;
            ContractorName = ContractorData.Name;
            City = AddressData.City;
            Street = AddressData.Street;
            HouseNumber = AddressData.HouseNumber;
            FlatNumber = AddressData.FlatNumber;
            if (!string.IsNullOrWhiteSpace(AddressData.FlatNumber))
            {
                FlatNumber = $"{Properties.Lang.Lang.House_Flat}" + AddressData.FlatNumber;
            }
            PostalCode = AddressData.PostalCode;
            Date = OrderData.OrderDate;
            Amount = OrderData.Amount;
            OrderProductsData = OrderProductsData;
        }

        

        #endregion

        #region Commands

        private ICommand printOrder;
        public ICommand PrintOrder
        {
            get
            {
                return printOrder ?? (printOrder = new RelayCommand(
                    (p) =>
                    {
                        printer.PrintOrder();                        
                    }
                    , p => true));
            }
        }

        #endregion
    }
}
