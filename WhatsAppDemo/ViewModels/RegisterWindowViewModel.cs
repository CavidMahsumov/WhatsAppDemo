using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WhatsAppDemo.Command;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.ViewModels
{
    public class RegisterWindowViewModel : BaseViewModel
    {

        public RelayCommand SendBtnCommand { get; set; }
        public static string VerifyCodee { get; set; }
        public RegisterWindowViewModel(RegisterPage registerPage)
        {
            SendBtnCommand = new RelayCommand((sender) =>
            {
                VerifyCodee = VerifyCode();


                //string accountSid = "ACd7a8f894246b1a5ec31f2c19e95e2db7";
                //string authToken = "beaecd89e5ef4b27fccfb168cf74b872";

                //TwilioClient.Init(accountSid, authToken);

                //var message = MessageResource.Create(
                //    body: $"{VerifyCodee}",
                //    from: new PhoneNumber("+16065179556"),
                //    to: new PhoneNumber($"+994{registerPage.NumberTxtBox.Text}")
                //);
                HelperClass.User = new Models.User();
                HelperClass.User.PhoneNumber = $"+994{registerPage.NumberTxtBox.Text}";

                VerifyNumberUserControl verifyNumberUserControl = new VerifyNumberUserControl(VerifyCodee);
                registerPage.MainGrid.Children.Add(verifyNumberUserControl);

            });
        }
        private string VerifyCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
