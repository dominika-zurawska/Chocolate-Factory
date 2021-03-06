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
        private OrderManager orderManager = null;

        private ObservableCollection<Order> orders = null;
        private int orderSelectedIndex = -1;

        #endregion

        #region Constructors
        public OrderDetailsViewModel OrderDetailsVM { get; set; }

        public TabOrdersHistoryViewModel(MainModel model, OrderManager orderManager)
        {
            this.model = model;
            this.orderManager = orderManager;
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
                            OrderDetailsVM = new OrderDetailsViewModel(model, orderManager, (int)orders[OrderSelectedIndex].Id);

                            var orderDetails = new View.OrderDetails(OrderDetailsVM);
                        },
                        arg => OrderSelectedIndex != -1
                        ) ;

                return openWindowOrderDetails;
            }
        }

        private ICommand repeatOrder = null;
        public ICommand RepeatOrder
        {
            get
            {
                if (repeatOrder == null)
                    repeatOrder = new RelayCommand(
                        arg =>
                        {
                            orderManager.RepeatOrder((int)orders[OrderSelectedIndex].Id);
                        },
                        arg => OrderSelectedIndex != -1
                        );

                return repeatOrder;
            }
        }

        #endregion
    }
}
