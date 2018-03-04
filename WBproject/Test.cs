using System;
using System.Collections.Generic;
using SODA;
using System.Net;
using System.IO;
using System.Text;

namespace WBproject
{
    public class Test
    {
        public Test()
        {

            var serviceURL = "https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyAeKNcZyUlkSoXS4KRiB3JxRkJYLst1Ef0";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceURL);
            request.Method = "POST";
            request.ContentType = "text/json; charset=utf-8";
            request.ContentLength = 0;
            //create requet body
            var values = new Dictionary<string, object>
            {
                {"macAddress","00:25:9c:cf:1c:ac"},
                {"signalStrength", -43},
                {"age", 0},
                {"channel", 11},
                {"signalToNoiseRatio", 0}
            };

            //
            HttpWebResponse serviceResponse = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);




            ////query
            ////https://data.cityoftacoma.org/resource/vzsr-722t.json?$where=occurred_on%20between%20%272018-01-10T12:00:00%27%20and%20%272018-03-02T14:00:00%27
            //var client = new SodaClient("soda.demo.socrata.com", "faxxyxOUEBkwIxlgvMgFaEViQ");

            //var dataset = client.GetResource<Dictionary<string, object>>("4tka-6guv");



            //var soql = new SoqlQuery().Select("earthquake_id")
            //                          .Where("magnitude > 3.0&source=pr");
            //var results = dataset.Query(soql);
            //foreach(var v in results)
            //{
            //    foreach(var vv in v)
            //    {
            //        Console.Write(vv);
            //    }
            //    Console.WriteLine();
            //}

        }
    }
}
