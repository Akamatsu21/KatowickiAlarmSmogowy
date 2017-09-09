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

namespace Hackathon24
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SmogDetails data = new SmogDetails();


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
            }, Dispatcher);

        }

        private void Hyperlink_RequestNavigate(object sender,
                                       System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}
