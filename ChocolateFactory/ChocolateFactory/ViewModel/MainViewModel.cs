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
    using System.Windows;


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


        private static bool isMessageBoxOpen = false;
        public static void showErrorDialog(Exception error) 
        {
            if (!isMessageBoxOpen) 
            {
                isMessageBoxOpen = true;
                var result = MessageBox.Show($"{Properties.Lang.Lang.ErrorMessageContent} {System.Environment.NewLine} " +
                    $"{Properties.Lang.Lang.ErrorMessageContentDetails} {System.Environment.NewLine} " + error.Message, 
                    $"{Properties.Lang.Lang.TitleErrorMessageBox}", MessageBoxButton.OK);
                
                if (result == MessageBoxResult.OK) 
                {
                    isMessageBoxOpen = false;
                }
            }

        }

    }
}
