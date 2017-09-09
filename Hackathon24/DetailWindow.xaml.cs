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
using System.Windows.Shapes;

namespace Hackathon24
{
    /// <summary>
    /// Logika interakcji dla klasy DetailWindow.xaml
    /// </summary>

    public class DataStruct
    {
        public string Message { get; set; }
    }

    public partial class DetailWindow : Window
    {
        public DetailWindow(string message)
        {
            InitializeComponent();
            DataContext = new DataStruct() { Message = message };
        }
    }
}
