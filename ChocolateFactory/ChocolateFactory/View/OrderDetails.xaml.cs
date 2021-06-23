using ChocolateFactory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChocolateFactory.View
{
    /// <summary>
    /// Logika interakcji dla klasy OrderDetails.xaml
    /// </summary>
    partial class OrderDetails : Window
    {
        internal OrderDetails(OrderDetailsViewModel OrderDetailsVM)
        {
            InitializeComponent();
            this.DataContext = OrderDetailsVM;
            this.ShowDialog();
        }
    }
}
