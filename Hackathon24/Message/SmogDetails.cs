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

        private string PickColor(int proc)
        {
            if (proc >= 200)
            {
                return "DarkRed";
            }
            else if (proc >= 100)
            {
                return "Red";
            }
            else if (proc >= 50)
            {
                return "Orange";
            }
            else if (proc >= 40)
            {
                return "Yellow";
            }
            else if (proc >= 20)
            {
                return "Green";
            }
            else
            {
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

            int max = (_p25Proc >= _p10Proc ? _p25Proc : _p10Proc);
            max = (max > _so2Proc ? max : _so2Proc);

            if (max >= 200)
            {
                state = "RYZYKOWNY";
                message = "Dla własnego bezpieczeństwa nie oddychaj.";
                image = "images\\nobreath.png";
                mainColor = Color.DARKRED;
            }
            else if (max >= 100)
            {
                state = "BARDZO ZŁY";
                message = "Zamknij okna i drzwi.";
                image = "images\\window.png";
                mainColor = Color.RED;
            }
            else if (max >= 50)
            {
                state = "ZŁY";
                message = "Lepiej zostań na kompie.";
                image = "images\\computer.png";
                mainColor = Color.ORANGE;
            }
            else if(max >= 40)
            {
                state = "ŚREDNI";
                message = "Kawa i ciastko z przyjacielem.";
                image = "images\\cake.png";
                mainColor = Color.YELLOW;
            }
            else if (max >= 20)
            {
                state = "DOBRY";
                message = "A może na spacerek?";
                image = "images\\stroll.png";
                mainColor = Color.GREEN;
            }
            else
            {
                state = "BARDZO DOBRY";
                message = "Bądź fit, idź biegać.";
                image = "images\\run.png";
                mainColor = Color.DARKGREEN;
            }

            p25Color = PickColor(_p25Proc);
            p10Color = PickColor(_p10Proc);
            so2Color = PickColor(_so2Proc);

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
