using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    using ChocolateFactory.View;
    using DAL.Entities;
    using DAL.Repositories;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    class Printer
    {
        internal void PrintOrder()
        {
            OrderDetails orderDetail = Application.Current.Windows.OfType<OrderDetails>().FirstOrDefault();
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)orderDetail.FlowDocument).DocumentPaginator, "Order");
            }
        }

    }
}
