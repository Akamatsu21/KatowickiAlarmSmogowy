using System;
using System.Collections.Generic;
using System.Text;
using Hackathon24.Message;
using System.Linq;

namespace Hackathon24.Message
{
    public class SmogDetails
    {
        public string so2 { get; set; }

        //Stan smogu i zwracana wiadomość
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

        //Teksty i kolory
        public string p25Text { get; set; }
        public string p10Text { get; set; }
        public string so2Text { get; set; }
        public string p25Color { get; set; }
        public string p10Color { get; set; }
        public string so2Color { get; set; }

        private Color mainColor;

        public SmogDetails()
        {
            so2 = "SO\u2082";
        }

        private Color GetColor(int proc, int p)
        {
            List<List<int> > numbers = new List<List<int> >();
            numbers.Add(new List<int>() { 200, 100, 50, 40, 20 });
            numbers.Add(new List<int>() { 120, 60, 30, 20, 10 });
            numbers.Add(new List<int>() { 155, 135, 125, 75, 40 });
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

        private string PickColor(Color c)
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

        public void Update()
        {
            _p10Value = Connection.GetP10Value();
            _p25Value = Connection.GetP25Value();
            _so2Value = Connection.GetSO2Value();
            _p25Proc = (int)(_p25Value % 25);
            _p10Proc = (int)(_p10Value % 50);
            _so2Proc = (int)(_so2Value % 350);

            int maxpos = (_p25Proc >= _p10Proc ? 0 : 1);
            int max = (_p25Proc >= _p10Proc ? _p25Proc : _p10Proc);
            maxpos = (max > _so2Proc ? maxpos : 2);
            max = (max > _so2Proc ? max : _so2Proc);

            mainColor = GetColor(max, maxpos);

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

            p25Color = PickColor(GetColor(_p25Proc, 0));
            p10Color = PickColor(GetColor(_p10Proc, 1));
            so2Color = PickColor(GetColor(_so2Proc, 2));

            p25Text = _p25Proc.ToString() + "%";
            p10Text = _p10Proc.ToString() + "%";
            so2Text = _so2Proc.ToString() + "%";
        }

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

        public Color GetMainColor()
        {
            return mainColor;
        }
         
    }
}
