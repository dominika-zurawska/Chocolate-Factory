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
        private ObservableCollection<Contractor> contractors = null;
        private ObservableCollection<Product> products = null;

        private Contractor selectedContractor;
        private Product selectedProduct;
        private ObservableCollection<Product> productsList = new ObservableCollection<Product>();
        private int productsListSelectedIndex;

        #endregion

        #region Constructors
        public TabOrderViewModel(MainModel model)
        {
            this.model = model;
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
                            // sprawdzić czy produkt nie jest pusty i czy nie jest już na liście produkt o takiej samej nazwie
                            ProductsList.Add(SelectedProduct);
                            // czyść formularz po dodaniu
                        },
                        arg => true
                        );

                return addProductToList;
            }
        }

        private ICommand loadForm = null;
        public ICommand LoadForm
        {
            get
            {
                if (loadForm == null)
                    loadForm = new RelayCommand(
                        arg =>
                        {
                            // do implementacji (do ładowania klikniętego elementu z listy do formularza potrzeba najpierw zbindowania każdego pola w formularzu)
                        },
                        arg => true
                        );

                return loadForm;
            }
        }


        #endregion
    }
}
