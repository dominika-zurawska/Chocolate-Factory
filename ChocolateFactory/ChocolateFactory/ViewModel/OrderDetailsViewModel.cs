using System;
using System.Collections.Generic;
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

        #endregion

        #region Constructors
        public OrderDetailsViewModel(MainModel model)
        {
            this.model = model;
        }
        #endregion

        #region Properties

        #endregion

        #region Commands

        #endregion
    }
}
