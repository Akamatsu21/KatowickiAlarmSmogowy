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
using KatowickiAlarmSmogowy.Message;
using System.Windows.Threading;
using System.Net.Mail;

namespace KatowickiAlarmSmogowy
{
    public enum Color
    {
        DARKRED,
        RED,
        ORANGE,
        YELLOW,
        GREEN,
        DARKGREEN
    }

    //Główne okno aplikacji
    public partial class MainWindow : Window
    {
        private SmogDetails data;                       //obiekt przechowujący dane o stanie powietrza
        private Dictionary<Color, String> LongMessages; //komentarze do poszczególnych stanów zanieczyszczenia
        private Color mainAlert;                        //kolor głównego komunikatu
        private SendEmail sendEmail;                    //obiekt wysyłający maile

        public MainWindow()
        {
            //Inicjalizacja i przypisanie zdarzeń dla przycisków
            InitializeComponent();
            p25Button.MouseLeftButtonDown += new MouseButtonEventHandler(p25_MouseLeftButtonDown);
            p10Button.MouseLeftButtonDown += new MouseButtonEventHandler(p10_MouseLeftButtonDown);
            so2Button.MouseLeftButtonDown += new MouseButtonEventHandler(so2_MouseLeftButtonDown);
            moreText.MouseLeftButtonDown += new MouseButtonEventHandler(moreText_MouseLeftButtonDown);
            businessCard.MouseLeftButtonDown += new MouseButtonEventHandler(businessCard_MouseLeftButtonDown);

            sendEmail = new SendEmail();
            data = new SmogDetails();
            data.Update(); //pobranie danych z API

            //Przypisanie komentarzy do odpowiadających kolorów
            LongMessages = new Dictionary<Color, string>();
            LongMessages[Color.DARKRED] = "Kobiety w ciąży, dzieci, osoby starsze oraz osoby cierpiące na astmę, choroby płuc, alergiczne choroby skóry i oczu oraz choroby krążenia(stany pozawałowe i zaburzenia rytmu serca) powinny bezwzględnie ograniczyć do minimum czas przebywania, a szczególnie unikać wysiłku fizycznego na otwartym powietrzu. Pozostałym zaleca się rezygnację z wysiłku fizycznego na otwartym powietrzu i zaniechanie palenia papierosów. W przypadku pogorszenia stanu zdrowia należy skontaktować się z lekarzem.";
            LongMessages[Color.RED] = "Kobiety w ciąży, dzieci, osoby starsze oraz osoby cierpiące na astmę, choroby płuc, alergiczne choroby skóry i oczu oraz choroby krążenia (stany pozawałowe i zaburzenia rytmu serca) powinny ograniczyć do minimum czas przebywania, a szczególnie unikać wysiłku fizycznego na otwartym powietrzu. Pozostałym zaleca się unikanie wysiłku fizycznego na otwartym powietrzu i ograniczenie palenia papierosów. W przypadku pogorszenia stanu zdrowia należy skontaktować się z lekarzem.";
            LongMessages[Color.ORANGE] = "Kobiety w ciąży, dzieci, osoby starsze oraz osoby cierpiące na astmę, choroby płuc, alergiczne choroby skóry i oczu oraz choroby krążenia(stany pozawałowe i zaburzenia rytmu serca) powinny ograniczyć czas przebywania oraz unikać wysiłku fizycznego na otwartym powietrzu. Pozostałym zaleca się zredukowanie czasu i intensywności wysiłku fizycznego na otwartym powietrzu.";
            LongMessages[Color.YELLOW] = "Kobiety w ciąży, dzieci, osoby starsze oraz osoby cierpiące na astmę, choroby płuc, alergiczne choroby skóry i oczu oraz choroby krążenia (stany pozawałowe i zaburzenia rytmu serca) powinny rozważyć ograniczenie wysiłku fizycznego na otwartym powietrzu.";
            LongMessages[Color.GREEN] = "Powietrze w stanie bardzo dobrym, może spacerek?";
            LongMessages[Color.DARKGREEN] = "Powietrze  w stanie idealnym! Zalecamy przebywanie na świeżym powietrzu i korzystanie z uroków życia :]";
            
            //Pobranie danych od nowa
            DispatcherTimer update = new System.Windows.Threading.DispatcherTimer();
            update.Tick += new EventHandler((o, e) =>
            {
                data.Update();
                mainAlert = data.GetMainColor();
                DataContext = data;
            });
            update.Start();
            update.Interval = new TimeSpan(0, 30, 0); //wykonuj co 30 minut

            //Wyślij email o 6:30 i 17:00
            //Ta część powinna być odkomentowana tylko w wersji admin!
            /*DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                dateBlock.Text = DateTime.Now.ToString("dd.MM.yyyy");
                timeBlock.Text = DateTime.Now.ToString("HH:mm:ss");

                if (timeBlock.Text == "06:30:00" || timeBlock.Text == "17:00:00")
                {
                    sendEmail.SendMail(LongMessages[mainAlert], data.p25Text, data.p10Text, data.so2Text);
                }
            }, Dispatcher);
            */
        }

        //Funkcja umożliwiająca przejście do adresu internetowego po kliknięciu Hyperlinku
        private void Hyperlink_RequestNavigate(object sender,
                                       System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        //Trzy funkcje wyślwietlające szczegółowe informacje po kliknięciu w kolorowe okręgi
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

        //Wyświetl szczegółowe informacje
        private void moreText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DetailWindow window = new DetailWindow(LongMessages[mainAlert]);
            window.Show();
        }

        //Wyświetl informacje kontaktowe
        private void businessCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContactWindow window = new ContactWindow();
            window.Show();
        }
    }
}
