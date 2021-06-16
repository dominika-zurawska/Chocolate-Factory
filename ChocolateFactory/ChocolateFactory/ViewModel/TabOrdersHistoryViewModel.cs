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

    class TabOrdersHistoryViewModel
    {
        #region Private components

        private MainModel model = null;

        #endregion

        #region Constructors
        public TabOrdersHistoryViewModel(MainModel model)
        {
            this.model = model;
        }
        #endregion
    }
}
