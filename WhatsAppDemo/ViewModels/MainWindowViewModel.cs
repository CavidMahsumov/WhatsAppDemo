using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WhatsAppDemo.Command;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;
using WpServer.Helper;

namespace WhatsAppDemo.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public RelayCommand ConnectBtnCommand { get; set; }
        public RelayCommand SearchBtnClick { get; set; }
        public RelayCommand DoubleClickCommand { get; set; }
        public DispatcherTimer dispatcherTimer { get; set; } = new DispatcherTimer();
        public MainWindow MainWindow { get; set; }
        public string Path { get; set; }
        public MainWindowViewModel(MainWindow mainWindow)
        {
          
           
            MainWindow = mainWindow;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
            HelperClass.mainWindow = mainWindow;

            SearchBtnClick = new RelayCommand((sender) =>
            {

                UserListUserControl userListUserControl = new UserListUserControl();
                mainWindow.MainGrid.Children.Add(userListUserControl);

            });
            DoubleClickCommand = new RelayCommand((sender) => {

                ChatUserControl chatUserControl = new ChatUserControl();
                mainWindow.MainGrid.Children.Add(chatUserControl);
            
            });
            ConnectBtnCommand = new RelayCommand((sender) =>
            {
                ChatUserControl chatUserControl = new ChatUserControl();
                mainWindow.MainGrid.Children.Add(chatUserControl);
                mainWindow.MainGrid.Children.RemoveAt(0);
                TcpClient client = new TcpClient();
                client.Connect(ConnectHelper.IpAdress, ConnectHelper.Port);
                HelperClass.Client = client;
                //HelperClass.Clients = new System.Collections.ObjectModel.ObservableCollection<TcpClient>();
                //HelperClass.Clients.Add(client);
                //Thread thread = new Thread(o => ReceiveData((TcpClient)o));
                //NetworkStream ns = client.GetStream();

                //thread.Start(client);
                //int cont = 0;
                //string s;
                //while (cont < 1)
                //{
                //    if (HelperClass.Clients.Count > 1)
                //    {

                //        ++cont;
                //        s = "User1";
                //        byte[] buffer = Encoding.ASCII.GetBytes(s);
                //        ns.Write(buffer, 0, buffer.Length);
                //        App.Current.Dispatcher.Invoke(() =>
                //        {
                //            //chatUserControl.MessageTxtBox.Text = null;
                //            //chatUserControl.HorizontalAlignment = HorizontalAlignment.Left;
                //            //chatUserControl.ChatListBox.Items.Add(chatUserControl.MessageTxtBox.Text);
                //        });
                //    }
                //}


            });
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
          
        }

        private void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                var msg = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);

               

            }


        }
    }
}
