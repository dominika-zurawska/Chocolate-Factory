using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.ViewModel
{
    using ChocolateFactory.Model;
    using BaseClass;
    using DAL;
    using System.Windows.Input;

    class MainViewModel
    {
        //creating an instance of the model
        private MainModel model = new MainModel();
        public OrderManager OrderMgr { get; set; }
        public TabOrderViewModel TabOrderVM { get; set; }
        public TabOrdersHistoryViewModel TabOrdersHistoryVM { get; set; }
        

        public MainViewModel()
        {
            OrderMgr = new OrderManager(model);
            TabOrderVM = new TabOrderViewModel(model, OrderMgr);
            TabOrdersHistoryVM = new TabOrdersHistoryViewModel(model, OrderMgr);
        }

    }
}
