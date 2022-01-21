using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;

namespace WhatsAppDemo.ViewModels
{
    public class MapUserControlViewModel:BaseViewModel
    {
        public MapUserControlViewModel(MapWindow mapUserControl)
        {
            var mapcontrol = new MapControl();
            mapcontrol.Loaded += Mapcontrol_Loaded;
            mapcontrol.MapServiceToken = App.Token;
            mapUserControl.MainGrid.Children.Add(mapcontrol);
        }

        private async void Mapcontrol_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            BasicGeoposition basicGeoposition = new BasicGeoposition() { Latitude = HelperClass.CurrentLocation[0], Longitude = HelperClass.CurrentLocation[1] };
            var center = new Geopoint(basicGeoposition);
            await ((MapControl)sender).TrySetViewAsync(center, 16);
                
        }
    }
}
