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

    class TabOrderViewModel : BaseViewModel
    {
        #region Private components

        private MainModel model = null;
        private ObservableCollection<Contractor> contractors = null;
        private ObservableCollection<Product> products = null;

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


        #endregion

        #region Commands

        #endregion
    }
}
