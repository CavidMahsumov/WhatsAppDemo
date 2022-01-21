using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppDemo.Command;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.ViewModels
{
    public class StartWindowViewMdoel:BaseViewModel
    {
        public RelayCommand AgreeAndContinueBtnCommand { get; set; }
        public List<User> Users { get; set; }
        public StartWindowViewMdoel(StartWindow startWindow)
        {
            AgreeAndContinueBtnCommand = new RelayCommand((sender) => {
                try
                {
                    Users = new List<User>();
                    if(File.Exists(@"C:\Users\mehsu\source\repos\WhatsAppDemo\WhatsAppDemo\bin\Debug\1.json"))
                    {
                       // Users.Add(JsonConvert.DeserializeObject<User>(File.ReadAllText(@"C:\Users\mehsu\source\repos\WhatsAppDemo\WhatsAppDemo\bin\Debug\1.json")));
                       HelperClass.MainUser = JsonConvert.DeserializeObject<User>(File.ReadAllText(@"C:\Users\mehsu\source\repos\WhatsAppDemo\WhatsAppDemo\bin\Debug\1.json"));
                    }
                    RegisterPage registerPage = new RegisterPage();
                    startWindow.Close();
                     registerPage.ShowDialog();

                }
                catch (Exception)
                {

                }
                         
            });
        }
    }
}
