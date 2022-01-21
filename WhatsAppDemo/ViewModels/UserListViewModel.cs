using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WhatsAppDemo.Command;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.ViewModels
{
    public class UserListViewModel : BaseViewModel
    {
        public RelayCommand AddBtnCommand { get; set; }
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }
        public Dictionary<TcpClient, User> dict { get; set; }
        public ObservableCollection<User> Users { get; set; }

        public UserListViewModel(UserListUserControl userListUserControl)
        {
            dict = new Dictionary<TcpClient, User>();
          
            try
            {
                userListUserControl.UserList.Items.Add(HelperClass.MainUser);
                //for (int i = 0; i < HelperClass.clientDict.Values.Count; i++)
                //{
                //    userListUserControl.UserList.Items.Add(HelperClass.clientDict.Values.ElementAt(i));
                //}
                //User User1 = JsonConvert.DeserializeObject<User>(File.ReadAllText(@"C:\Users\mehsu\source\repos\WhatsAppDemo\WhatsAppDemo\bin\Debug\1.json"));
                //userListUserControl.UserList.Items.Add(User1);
            }
            catch (Exception ex)
            {

            }
            AddBtnCommand = new RelayCommand((sender) =>
            {

                try
                {

                    HelperClass.User = User;
                    HelperClass.mainWindow.Listbox.Items.Add(User);
                    HelperClass.mainWindow.MainGrid.Children.RemoveAt(1);
                }
                catch (Exception)
                {

                    throw;
                }

            });


        }

    }


}
