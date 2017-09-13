using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using KatowickiAlarmSmogowy.Message;

namespace KatowickiAlarmSmogowy.Message
{
    //Klasa do pobierania danych z API
    public static class Connection
    {
        //Pobranie danych w JSON-ie
        private static string httpConection(DustType id)
        {
            int temp = (int)id;
            string url = "http://api.gios.gov.pl/pjp-api/rest/data/getData/" + temp.ToString();
            return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        //Deserializacja JSON-a do odpowiadającyh klas
        private static P10Wrapper JsonDeserializeToP10(string url)
        {
            return JsonConvert.DeserializeObject<P10Wrapper>(url);
        }
        private static P25Wrapper JsonDeserializeToP25(string url)
        {
            return JsonConvert.DeserializeObject<P25Wrapper>(url);
        }
        private static SO2Wrapper JsonDeserializeToSO2(string url)
        {
            return JsonConvert.DeserializeObject<SO2Wrapper>(url);
        }

        //Zwrócenie interesującyhc nas wartości
        public static double? GetP10Value()
        {
            return Connection.JsonDeserializeToP10(Connection.httpConection(DustType.PM10)).values.
                Where(p => p.value != null).Select(p => p.value).FirstOrDefault();
        }

        public static double? GetP25Value()
        {
            return Connection.JsonDeserializeToP25(Connection.httpConection(DustType.PM25)).values.
                Where(p => p.value != null).Select(p => p.value).FirstOrDefault();
        }

        public static double? GetSO2Value()
        {
            return Connection.JsonDeserializeToSO2(Connection.httpConection(DustType.SO2)).values.
                Where(p => p.value != null).Select(p => p.value).FirstOrDefault();
        }
    }
}
