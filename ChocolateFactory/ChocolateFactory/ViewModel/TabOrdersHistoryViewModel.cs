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

    class TabOrdersHistoryViewModel:BaseViewModel
    {
        #region Private components

        private MainModel model = null;
        private ObservableCollection<Order> orders = null;

        private int orderSelectedIndex = -1;

        #endregion

        #region Constructors
        public TabOrdersHistoryViewModel(MainModel model)
        {
            this.model = model;
            orders = model.Orders;
        }
        #endregion

        #region Properties

        public ObservableCollection<Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                onPropertyChanged(nameof(Orders));
            }
        }

        public int OrderSelectedIndex
        {
            get { return orderSelectedIndex; }
            set
            {
                orderSelectedIndex = value;
                onPropertyChanged(nameof(OrderSelectedIndex));
            }
        }


        #endregion

        #region Commands

        private ICommand openWindowOrderDetails = null;
        public ICommand OpenWindowOrderDetails
        {
            get
            {
                if (openWindowOrderDetails == null)
                    openWindowOrderDetails = new RelayCommand(
                        arg =>
                        {
                            var orderDetails = new View.OrderDetails();
                            orderDetails.Show();
                        },
                        arg => OrderSelectedIndex != -1
                        ) ;

                return openWindowOrderDetails;
            }
        }

        #endregion
    }
}
