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
using Hackathon24.Message;
using System.Windows.Threading;
using System.Net.Mail;

namespace Hackathon24
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SmogDetails data;

        public MainWindow()
        {
            InitializeComponent();
            p25Button.MouseLeftButtonDown += new MouseButtonEventHandler(p25_MouseLeftButtonDown);
            p10Button.MouseLeftButtonDown += new MouseButtonEventHandler(p10_MouseLeftButtonDown);
            so2Button.MouseLeftButtonDown += new MouseButtonEventHandler(so2_MouseLeftButtonDown);

            data = new SmogDetails();


            DispatcherTimer update = new System.Windows.Threading.DispatcherTimer();
            update.Tick += new EventHandler((o,e) => {
                data.Update();
                DataContext = data;
            });
            update.Start();
            update.Interval = new TimeSpan(0, 30, 0);


            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                dateBlock.Text = DateTime.Now.ToString("dd.MM.yyyy");
                timeBlock.Text = DateTime.Now.ToString("HH:mm:ss");

                if (timeBlock.Text == "06:30:00" || timeBlock.Text == "17:00:00")
                {
                    SendEmail.SendMail();
                }


            }, Dispatcher);

        }

        private void Hyperlink_RequestNavigate(object sender,
                                       System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void p25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InfoWindow window = new InfoWindow();
            window.Title.Text = "Pył zawieszony PM 2.5";
            window.Current.Text = "Aktualna wartość: " + data.GetValue(25).ToString();
            window.Norm.Text = "Norma: 25";
            window.Diff.Text = "Przekroczenie: " + (350 - data.GetValue(25)).ToString();
            window.Show();
        }

        private void p10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InfoWindow window = new InfoWindow();
            window.Title.Text = "Pył zawieszony PM 10";
            window.Current.Text = "Aktualna wartość: " + data.GetValue(10).ToString();
            window.Norm.Text = "Norma: 50";
            window.Diff.Text = "Przekroczenie: " + (50 - data.GetValue(10)).ToString();
            window.Show();
        }

        private void so2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InfoWindow window = new InfoWindow();
            window.Title.Text = "Dwutlenek siarki SO\u2082";
            window.Current.Text = "Aktualna wartość: " + data.GetValue(2).ToString();
            window.Norm.Text = "Norma: 350";
            window.Diff.Text = "Przekroczenie: " + (10 - data.GetValue(2)).ToString();
            window.Show();
        }
    }
}
