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
    using ChocolateFactory.ViewModel.BaseClass;
    using System.Windows.Input;

    class TabOrderViewModel : BaseViewModel
    {
        #region Private components

        private MainModel model = null;
        private OrderManager orderManager = null;
        private ObservableCollection<Contractor> contractors = null;
        private ObservableCollection<Product> products = null;
        private decimal amount = 0;

        private Contractor selectedContractor;
        private Product selectedProduct;
        private ObservableCollection<Product> productsList = new ObservableCollection<Product>();
        private int productsListSelectedIndex = -1;
        private int quantity;
        private bool selectedAdmin;
        private bool selectedUser;
        private string tabVisible = $"{Properties.Lang.Lang.TabVisible}";

        #endregion

        #region Constructors
        public TabOrderViewModel(MainModel model, OrderManager orderManager)
        {
            this.model = model;
            this.orderManager = orderManager;
            contractors = model.Contractors;
            products = model.Products;
        }
        #endregion


        #region Properties

        public ObservableCollection<Contractor> Contractors
        {
            get { return contractors; }
            set
            {
                contractors = value;
                onPropertyChanged(nameof(Contractors));
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                onPropertyChanged(nameof(Products));
            }
        }

        public Contractor SelectedContractor
        {
            get { return selectedContractor; }
            set
            {
                selectedContractor = value;
                onPropertyChanged(nameof(SelectedContractor));
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                onPropertyChanged(nameof(SelectedProduct));
            }
        }

        public ObservableCollection<Product> ProductsList
        {
            get { return productsList; }
            set
            {
                productsList = value;
                onPropertyChanged(nameof(ProductsList));
            }
        }

        public int ProductsListSelectedIndex
        {
            get { return productsListSelectedIndex; }
            set
            {
                productsListSelectedIndex = value;
                onPropertyChanged(nameof(ProductsListSelectedIndex));
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                onPropertyChanged(nameof(Quantity));
            }
        }

        public string TabVisible
        {
            get { return tabVisible; }
            set
            {
                tabVisible = value;
                onPropertyChanged(nameof(TabVisible));
            }
        }


        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                onPropertyChanged(nameof(Amount));
            }
        }

        public bool SelectedAdmin
        {
            get { return selectedAdmin; }
            set
            {
                selectedAdmin = value;
                onPropertyChanged(nameof(SelectedAdmin));
            }
        }
        public bool SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                onPropertyChanged(nameof(SelectedUser));
            }
        }


        #endregion

        #region Commands



        private ICommand addProductToList = null;
        public ICommand AddProductToList
        {
            get
            {
                if (addProductToList == null)
                    addProductToList = new RelayCommand(
                        arg =>
                        {
                            orderManager.AddPosition(SelectedProduct, Quantity, ProductsList);
                            Amount = orderManager.CountAmount(ProductsList);
                        },
                        arg => SelectedProduct != null && Quantity > 0 && !ProductsList.Contains(SelectedProduct)
                        );

                return addProductToList;
            }
        }

        private ICommand deleteProductFromList = null;
        public ICommand DeleteProductFromList
        {
            get
            {
                if (deleteProductFromList == null)
                    deleteProductFromList = new RelayCommand(
                        arg =>
                        {
                            orderManager.DeletePosition(ProductsList, ProductsListSelectedIndex);
                            Amount = orderManager.CountAmount(ProductsList);
                        },
                        arg => ProductsListSelectedIndex > -1
                        );

                return deleteProductFromList;
            }
        }

        private ICommand loadItemToForm = null;
        public ICommand LoadItemToForm
        {
            get
            {
                if (loadItemToForm == null)
                    loadItemToForm = new RelayCommand(
                        arg =>
                        {
                            SelectedProduct = Products[(int)ProductsList[ProductsListSelectedIndex].Id - 1];
                            Quantity = ProductsList[ProductsListSelectedIndex].Quantity;
                        },
                        arg => ProductsListSelectedIndex > -1
                        );

                return loadItemToForm;
            }
        }

        private ICommand editProductOnList = null;
        public ICommand EditProductOnList
        {
            get
            {
                if (editProductOnList == null)
                    editProductOnList = new RelayCommand(
                        arg =>
                        {
                            orderManager.EditPosition(ProductsList, ProductsListSelectedIndex, SelectedProduct, Quantity);
                            Amount = orderManager.CountAmount(ProductsList);
                        },
                        arg => ProductsListSelectedIndex > -1 && Quantity > 0 && Quantity != ProductsList[ProductsListSelectedIndex].Quantity
                        );

                return editProductOnList;
            }
        }

        private ICommand submitOrder = null;
        public ICommand SubmitOrder
        {
            get
            {
                if (submitOrder == null)
                    submitOrder = new RelayCommand(
                        arg =>
                        {
                            orderManager.SubmitOrder(SelectedContractor, ProductsList);
                            ProductsListSelectedIndex = -1;
                            ProductsList = new ObservableCollection<Product>();
                            Amount = 0;
                            Quantity = 0;
                            SelectedContractor = null;
                            SelectedProduct = null;
                            Products = model.Products;
                        },
                        arg => SelectedContractor != null && ProductsList.Count > 0
                        );

                return submitOrder;
            }
        }

        private ICommand setVisibility = null;
        public ICommand SetVisibility
        {
            get
            {
                return setVisibility ?? (setVisibility = new RelayCommand(
                    (p) =>
                    {
                        if (SelectedUser)
                        {
                            TabVisible = $"{Properties.Lang.Lang.TabHidden}";
                        }
                        else
                        {
                            TabVisible = $"{Properties.Lang.Lang.TabVisible}";
                        }
                    }
                    , p => true));
            }
        }


        #endregion
    }
}
