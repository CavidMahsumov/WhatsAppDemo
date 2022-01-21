using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppDemo.Command;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.ViewModels
{
    class VerifyNumberViewModel : BaseViewModel
    {
        public RelayCommand SendBtnCommand { get; set; }
        public VerifyNumberViewModel(VerifyNumberUserControl verifyNumberUserControl,string VerifyCode)
        {
            verifyNumberUserControl.VerifyNumbertxt.Text = HelperClass.User.PhoneNumber;
            verifyNumberUserControl.VerifyNumbertxt2.Text = HelperClass.User.PhoneNumber;
            SendBtnCommand = new RelayCommand((sender) =>
            {
                //if (verifyNumberUserControl.CodeTextBox.Text == VerifyCode)
                //{
                    FillProfileUserControl fillProfileUserControl = new FillProfileUserControl();
                    verifyNumberUserControl.MainGrid.Children.Add(fillProfileUserControl);

                //}
                //else
                //{
                //    MessageBox.Show("Wrong Code.Try Again");
                //}


            });
        }
    }
}
