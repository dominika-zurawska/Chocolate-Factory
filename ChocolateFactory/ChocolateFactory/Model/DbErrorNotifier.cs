using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Model
{
    class DbErrorNotifier
    {
        internal static void notifyError(Exception error) 
        {
            ViewModel.MainViewModel.showErrorDialog(error);
        }
    }
}
