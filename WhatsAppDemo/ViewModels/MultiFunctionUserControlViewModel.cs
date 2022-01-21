using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppDemo.Command;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Helper.ImageHelper;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.ViewModels
{
    public class MultiFunctionUserControlViewModel : BaseViewModel
    {
        public RelayCommand LocationBtnCommand { get; set; }
        public RelayCommand ImageBtnCommand { get; set; }
        public RelayCommand PdfBtnCommand { get; set; }
        public string Path1 { get; set; }

        private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

        public MultiFunctionUserControlViewModel(MultiFunctionUserControl multiFunctionUserControl)
        {
            LocationBtnCommand = new RelayCommand((sender) =>
            {

                CLocation myLocation = new CLocation();
                myLocation.GetLocationEvent();

                App.Current.Dispatcher.Invoke(() =>
                {

                    HelperClass.chatUserControl.MessageTxtBox.Text =$"Location";
                    HelperClass.chatUserControl.Gridd.Children.RemoveAt(1);

                });

                //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

                //// Do not suppress prompt, and wait 1000 milliseconds to start.
                //watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

                //GeoCoordinate coord = watcher.Position.Location;

                //if (coord.IsUnknown)
                //{
                //    MessageBox.Show(coord.Latitude.ToString());
                //    MessageBox.Show(coord.Longitude.ToString());
                //}
                //else
                //{
                //    Console.WriteLine("Unknown latitude and longitude.");
                //}
            });
            PdfBtnCommand = new RelayCommand((sender) =>
            {

                try
                {
                    var open = new Microsoft.Win32.OpenFileDialog();

                    open.Multiselect = false;
                    open.Filter = "Pdf Files (*.pdf)|*.pdf;";
                    open.ShowDialog();
                    //Text = PdfHelper.ReadPdfFile(open.FileName);

                    if (".pdf".Equals(Path.GetExtension(open.FileName), StringComparison.OrdinalIgnoreCase))
                    {
                        Path1 = open.FileName;
                        HelperClass.chatUserControl.MessageTxtBox.Text = Path1;
                        HelperClass.chatUserControl.Gridd.Children.RemoveAt(1);


                    }
                    else
                    {
                        Path1 = open.FileName;

                    }

                    //mainWindow.image1.Source = new BitmapImage(new Uri(open.FileName));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            ImageBtnCommand = new RelayCommand((sender) => {
                try
                {
                    var open = new Microsoft.Win32.OpenFileDialog();

                    open.Multiselect = false;
                    open.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
                    open.ShowDialog();
                    //Text = PdfHelper.ReadPdfFile(open.FileName);

                    if (".jpg".Equals(Path.GetExtension(open.FileName), StringComparison.OrdinalIgnoreCase))
                    {
                        Path1 = open.FileName;
                        var Path=IHelper.GetImagePath(IHelper.GetBytesOfImage(Path1), 2);
                        HelperClass.chatUserControl.MessageTxtBox.Text = Path;
                        HelperClass.chatUserControl.Gridd.Children.RemoveAt(1);


                    }
                    else
                    {
                        Path1 = open.FileName;

                    }

                    //mainWindow.image1.Source = new BitmapImage(new Uri(open.FileName));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });
        }
    }
}
