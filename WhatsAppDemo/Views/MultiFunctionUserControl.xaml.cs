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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsAppDemo.ViewModels;

namespace WhatsAppDemo.Views
{
    /// <summary>
    /// Interaction logic for MultiFunctionUserControl.xaml
    /// </summary>
    public partial class MultiFunctionUserControl : UserControl
    {
        public MultiFunctionUserControl()
        {
            InitializeComponent();
            this.DataContext = new MultiFunctionUserControlViewModel(this);
        }
    }
}
