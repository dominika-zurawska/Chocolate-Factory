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

        private Contractor selectedContractor;
        private Product selectedProduct;
        private ObservableCollection<Product> productsList = new ObservableCollection<Product>();
        private int productsListSelectedIndex = -1;
        private int quantity;

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
                        },
                        arg => SelectedProduct != null && Quantity > 0
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
                            SelectedProduct = ProductsList[ProductsListSelectedIndex];
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
                            ProductsList[ProductsListSelectedIndex] = orderManager.EditPosition(ProductsList, ProductsListSelectedIndex, SelectedProduct, Quantity);           
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
                        },
                        arg => SelectedContractor != null && ProductsList.Count > 0
                        );

                return submitOrder;
            }
        }


        #endregion
    }
}
