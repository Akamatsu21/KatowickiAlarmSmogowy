using System;
using System.Collections.Generic;
using System.Text;
using KatowickiAlarmSmogowy.Message;
using System.Linq;

namespace KatowickiAlarmSmogowy.Message
{
    //Klasa zawierająca informacje o smogu do zbindowania w głównym oknie
    public class SmogDetails
    {
        //Zmienna z tekstem "SO2" (wyświetlana przez binding bo w XAML-u nie można wyświetlić indeksu dolnego)
        public string so2 { get; set; }

        //Stan smogu, komunikat i obrazek
        public string state { get; set; }
        public string message { get; set; }
        public string image { get; set;  }

        //Wartości liczbowe i procentowe
        private double? _p25Value { get; set; }
        private int _p25Proc { get; set; }
        private double? _p10Value { get; set; }
        private int _p10Proc { get; set; }
        private double? _so2Value { get; set; }
        private int _so2Proc { get; set; }

        //Teksty i kolory okręgów
        public string p25Text { get; set; }
        public string p10Text { get; set; }
        public string so2Text { get; set; }
        public string p25Color { get; set; }
        public string p10Color { get; set; }
        public string so2Color { get; set; }

        //Kolor głównego komunikatu
        private Color mainColor;

        public SmogDetails()
        {
            so2 = "SO\u2082"; //przypisz wartość tekstową do wyświetlenia
        }

        //Zwróć kolor na podstawie procenta zanieczyszczeń
        private Color GetColor(int proc, int p)
        {
            List<List<int> > numbers = new List<List<int> >();
            numbers.Add(new List<int>() { 200, 100, 50, 40, 20 });  //wartości dla PS 2.5
            numbers.Add(new List<int>() { 120, 60, 30, 20, 10 });   //wartości dla PS 10
            numbers.Add(new List<int>() { 155, 135, 125, 75, 40 }); //wartości dla SO2
            if (proc >= numbers[p][0])
            {
                return Color.DARKRED;
            }
            else if (proc >= numbers[p][1])
            {
                return Color.RED;
            }
            else if (proc >= numbers[p][2])
            {
                return Color.ORANGE;
            }
            else if (proc >= numbers[p][3])
            {
                return Color.YELLOW;
            }
            else if (proc >= numbers[p][4])
            {
                return Color.GREEN;
            }
            else
            {
                return Color.DARKGREEN;
            }
        }

        //Zwróć kolor jako string (konieczne bo XAML przyjmuje kolory jako stringi)
        private string ColorToString(Color c)
        {
            switch(c)
            {
                case Color.DARKRED:
                    return "DarkRed";
                case Color.RED:
                    return "Red";
                case Color.ORANGE:
                    return "Orange";
                case Color.YELLOW:
                    return "Yellow";
                case Color.GREEN:
                    return "Green";
                default:
                    return "DarkGreen";
            }
        }

        //Funkcja pobierająca dane i przygotowująca zmienne do wyświetlenia
        public void Update()
        {
            //Pobierz wartości i zanieczyszczeń i konwertuej do procentów
            _p10Value = Connection.GetP10Value();
            _p25Value = Connection.GetP25Value();
            _so2Value = Connection.GetSO2Value();
            _p25Proc = (int)(_p25Value % 25);
            _p10Proc = (int)(_p10Value % 50);
            _so2Proc = (int)(_so2Value % 350);

            //max to najwyższy procent, maxpos to jego pozycja w tablicy (0, 1 lub 2)
            int maxpos = (_p25Proc >= _p10Proc ? 0 : 1);
            int max = (_p25Proc >= _p10Proc ? _p25Proc : _p10Proc);
            maxpos = (max > _so2Proc ? maxpos : 2);
            max = (max > _so2Proc ? max : _so2Proc);
            mainColor = GetColor(max, maxpos);

            //Na podstawie koloru głównego komunikatu, przygotuj treść
            switch(mainColor)
            {
                case Color.DARKRED:
                    state = "RYZYKOWNY";
                    message = "Dla własnego bezpieczeństwa nie oddychaj.";
                    image = "images\\nobreath.png";
                    break;
                case Color.RED:
                    state = "BARDZO ZŁY";
                    message = "Zamknij okna i drzwi.";
                    image = "images\\window.png";
                    break;
                case Color.ORANGE:
                    state = "ZŁY";
                    message = "Lepiej zostań na kompie.";
                    image = "images\\computer.png";
                    break;
                case Color.YELLOW:
                    state = "ŚREDNI";
                    message = "Kawa i ciastko z przyjacielem.";
                    image = "images\\cake.png";
                    break;
                case Color.GREEN:
                    state = "DOBRY";
                    message = "A może na spacerek?";
                    image = "images\\stroll.png";
                    break;
                default:
                    state = "BARDZO DOBRY";
                    message = "Bądź fit, idź biegać.";
                    image = "images\\run.png";
                    break;
            }

            //Zmień kolory na stringi
            p25Color = ColorToString(GetColor(_p25Proc, 0));
            p10Color = ColorToString(GetColor(_p10Proc, 1));
            so2Color = ColorToString(GetColor(_so2Proc, 2));

            //Zmień liczby na stringi
            p25Text = _p25Proc.ToString() + "%";
            p10Text = _p10Proc.ToString() + "%";
            so2Text = _so2Proc.ToString() + "%";
        }

        //Zwróć wartość liczbową danego typu zanieczyszczenia
        public double? GetValue(int p)
        {
            switch(p)
            {
                case 25:
                    return _p25Value;
                case 10:
                    return _p10Value;
                case 2:
                    return _so2Value;
                default:
                    return null;
            }
        }

        //Zwróć główny kolor
        public Color GetMainColor()
        {
            return mainColor;
        }
         
    }
}
