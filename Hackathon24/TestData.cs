using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon24
{
    class TestData
    {
        public string City { get; set; }
        public string District { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public string State { get; set; }
        public string Message { get; set; }

        public string PM25 { get; set; }
        public string PM25Color { get; set; }
        public string PM10 { get; set; }
        public string PM10Color { get; set; }
        public string SO2 { get; set; }
        public string SO2Color { get; set; }

        public static TestData GetTestData()
        {
            var data = new TestData()
            {
                City = "Katowice",
                District = "Kossutha",

                Date = "09.09.2017",
                Time = "14:00",

                State = "Bardzo zły",
                Message = "Lepiej zostań przed kompem",

                PM25 = "PM 2.5%" + Environment.NewLine + "50%",
                PM25Color = "Red",
                PM10 = "PM 10%" + Environment.NewLine + "0%",
                PM10Color = "Green",
                SO2 = "SO\u2082" + Environment.NewLine + "0%",
                SO2Color = "Orange"
            };
            return data;
        }
    }
}
