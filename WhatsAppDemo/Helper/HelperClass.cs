using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.Helper
{
   public  class HelperClass
    {
        public static TcpClient Client { get; set; }
        public static  double[] CurrentLocation { get; set; } = new double[2];
        public static ChatUserControl chatUserControl { get; set; }
        public static MainWindow mainWindow { get; set; }
        public static User User { get; set; } 

        public static  ObservableCollection<TcpClient> Clients  { get; set; }
        public static Dictionary<TcpClient, User> clientDict;
        public static User MainUser { get; set; }
        public static StartWindow startWindow { get; set; }
        public static RegisterPage register { get; set; }
        public HelperClass()
        {
            User = new User();
            MainUser = new User();
        }
    }
}
