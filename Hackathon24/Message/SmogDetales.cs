using System;
using System.Collections.Generic;
using System.Text;
using Hackatohon24.Message;
using System.Linq;

namespace Hackathon24.Message
{
    public class SmogDetales
    {

        private double? _p25Value { get; set; }
        private int _p25Proc { get; set; }
        private double? _p10Value { get; set; }
        private int _p10Proc { get; set; }
        private double? _so2Value { get; set; }
        private int _so2Proc { get; set; }

        public void Update()
        {
            _p10Value = Connection.GetP10Value();
            _p25Value = Connection.GetP25Value();
            _so2Value = Connection.GetSO2Value();
            _p10Proc = (int)(_p10Value % 50);
            _p25Proc = (int)(_p25Value % 25);
            _so2Proc = (int)(_so2Value % 350);
        }
    }
}
