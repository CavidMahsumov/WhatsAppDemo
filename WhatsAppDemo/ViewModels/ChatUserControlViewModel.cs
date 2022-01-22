using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WhatsAppDemo.Command;
using WhatsAppDemo.Extension;
using WhatsAppDemo.Helper;
using WhatsAppDemo.Models;
using WhatsAppDemo.Views;
using WpServer.Helper;

namespace WhatsAppDemo.ViewModels
{
    public class ChatUserControlViewModel : BaseViewModel
    {
        public RelayCommand SendBtnCommand { get; set; }
        public RelayCommand MultiFunctionallyButton { get; set; }
        public ChatUserControl ChatUserControl { get; set; }
        public RelayCommand MouseDoubleClickCommand { get; set; }
        public RelayCommand VoiceRecordBtnCommand { get; set; }
        private object message;

        public object Message
        {
            get { return message; }
            set { message = value; }
        }
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        public DispatcherTimer DispatcherTimer { get; set; } = new DispatcherTimer();

        public int Count { get; set; }
        public string VoicePath { get; set; }
        public ChatUserControlViewModel(ChatUserControl chatUserControl)
        {
            DispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            DispatcherTimer.Tick += DispatcherTimer_Tick;

            try
            {

                App.Current.Dispatcher.Invoke(() =>
                {

                    chatUserControl.Username.Text = HelperClass.MainUser.Name;
                    chatUserControl.UserImg.Source = new BitmapImage(new Uri(HelperClass.MainUser.ImagePath));

                });
            }
            catch (Exception)
            {

            }
            ChatUserControl = chatUserControl;
            HelperClass.chatUserControl = chatUserControl;
            SendBtnCommand = new RelayCommand((sender) =>
            {
                try
                {
                    if (HelperClass.Client.Connected)
                    {
                        Console.WriteLine("client connected!!");

                        Thread thread = new Thread(o => ReceiveData((TcpClient)o));
                        NetworkStream ns = HelperClass.Client.GetStream();

                        thread.Start(HelperClass.Client);

                        string s;
                        while (!string.IsNullOrEmpty((s = chatUserControl.MessageTxtBox.Text)))
                        {
                            if (chatUserControl.MessageTxtBox.Text == "Location")
                            {
                                // string filepath1 = @"C:\Users\mehsu\source\repos\WhatsAppDemo\WhatsAppDemo\bin\Debug\Location1.json";//Check
                                //var Location = new Location { ImagePath = "../Images/Location.png", Latitude = HelperClass.CurrentLocation[0], Longitude = HelperClass.CurrentLocation[1] };
                                //string jsonstr = JsonSerializer.Serialize(Location);
                                //File.WriteAllText(filepath1, jsonstr);

                                byte[] buffer = Encoding.ASCII.GetBytes("Location");
                                ns.Write(buffer, 0, buffer.Length);
                                chatUserControl.MessageTxtBox.Text = null;
                            }
                            else
                            {
                                byte[] buffer = Encoding.ASCII.GetBytes(s);
                                ns.Write(buffer, 0, buffer.Length);
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    chatUserControl.MessageTxtBox.Text = null;
                                    chatUserControl.HorizontalAlignment = HorizontalAlignment.Left;
                                    chatUserControl.ChatListBox.Items.Add(chatUserControl.MessageTxtBox.Text);
                                    File.WriteAllText(@"C:\Users\mehsu\source\repos\WhatsAppDemo\WhatsAppDemo\bin\Debug\Messages.txt", s);
                                });
                            }

                        }

                        //HelperClass.Client.Client.Shutdown(SocketShutdown.Send);
                        //thread.Join();
                        //ns.Close();

                    }
                    else
                    {
                        MessageBox.Show("Connect Failed!Check Ip or Port");
                    }
                }
                catch (Exception ex)
                {

                }




            });
            MultiFunctionallyButton = new RelayCommand((sender) =>
            {
                try
                {

                    MultiFunctionUserControl multiFunctionUserControl = new MultiFunctionUserControl();
                    chatUserControl.Gridd.Children.Add(multiFunctionUserControl);

                }
                catch (Exception ex)
                {

                }

            });
            MouseDoubleClickCommand = new RelayCommand((sender) =>
            {
                try
                {
                    if (message is Voice)
                    {
                        Process.Start("d:\\mic.wav");

                    }
                    else if (message is PDF)
                    {
                        var pdf = (PDF)message;
                        Process.Start(pdf.FilePath);
                    }
                    else if (message is Images)
                    {
                        var image = (Images)message;
                        Process.Start(image.ImagePath);
                    }
                    else if (message is Location)
                    {
                        MapWindow mapWindow = new MapWindow();
                        mapWindow.ShowDialog();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });
            VoiceRecordBtnCommand = new RelayCommand((sender) =>
            {
                ++Count;
                if (Count % 2 == 0)
                {
                    DispatcherTimer.Stop();
                    record("save recsound C:\\mic.wav", "", 0, 0);
                    record("close recsound", "", 0, 0);
                    VoicePath = "C:\\mic.wav";
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        // chatUserControl.ChatListBox.Items.Add(new Voice(VoicePath, "../Images/voicemsg.png"));
                        chatUserControl.MessageTxtBox.Text = VoicePath;

                    });
                }
                else
                {
                    DispatcherTimer.Start();
                    record("open new Type waveaudio Alias recsound", "", 0, 0);
                    record("record recsound", "", 0, 0);

                }


            });
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ChatUserControl.Timer.Text = DateTime.Now.ToString("HH:mm:ss");



        }

        private void ReceiveData(TcpClient client)
        {
            try
            {
                NetworkStream ns = client.GetStream();
                byte[] receivedBytes = new byte[1024];
                int byte_count;
                ;

                while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    var msg = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
                    string FileExtension = System.IO.Path.GetExtension(msg);
                    if (FileExtension == ".pdf")
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {

                            ChatUserControl.ChatListBox.Items.Add(new PDF("../Images/Pdf.png", msg));

                        });
                    }
                    else if (FileExtension == ".png" || FileExtension == ".jpg" || FileExtension == ".jpeg")
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {

                            ChatUserControl.ChatListBox.Items.Add(new Images(msg));


                        });

                    }
                    else if (FileExtension == ".wav")
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {

                            ChatUserControl.ChatListBox.Items.Add(new Voice(VoicePath, "../Images/voicemsg.png"));
                        });
                    }
                    else if (msg=="Location")
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {

                            ChatUserControl.ChatListBox.Items.Add(new Location { ImagePath = "../Images/Location.png" });

                        });
                    }
                    else
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {

                            ChatUserControl.ChatListBox.Items.Add(new Message(msg, DateTime.Now));
                            ChatUserControl.ChatListBox.HorizontalContentAlignment = HorizontalAlignment.Right;


                        });
                    }



                }
            }
            catch (Exception)
            {

            }


        }


    }

}
