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

namespace KatowickiAlarmSmogowy
{
    public class DataStruct
    {
        public string Message { get; set; }
    }

    //Okno wyświetlające tekst przekazany jako argument do konstruktora
    public partial class DetailWindow : Window
    {
        public DetailWindow(string message)
        {
            InitializeComponent();
            DataContext = new DataStruct() { Message = message };
        }
    }
}
