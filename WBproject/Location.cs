using System;
using System.IO;
using System.Net;
using System.Text;


namespace WBproject
{
    public class Location
    {
        public string city;
        public string state;
        public float longitude;
        public float latitude;
        public string zipcode;



        public void setLocation(string city, string state, float longitude, float latitude, string zipcode)
        {
            this.city = city;
            this.state = state;
            this.longitude = longitude;
            this.latitude = latitude;
            this.zipcode = zipcode;
        }



        public void get_Location()
        {
            get_Location(null);
        }
        public void get_Location(IPAddress ip)
        {
            
            var serviceURL = "http://freegeoip.net/json/";
            if (ip==null) serviceURL += ip;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceURL);
            request.Method = "GET";
            request.ContentType = "text/xml; charset=utf-8";
            request.ContentLength = 0;

            HttpWebResponse serviceResponse = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);

            string serviceResult = readStream.ReadToEnd();


            foreach (var v in serviceResult.Split(","))
            {
                //Console.WriteLine(v);
                if (v.IndexOf("city")!=-1)
                    city=v.Substring("city".Length+3).Trim('"');
                if (v.IndexOf("region_name") != -1)
                    state = v.Substring("region_name".Length+3).Trim('"');
                if (v.IndexOf("zip_code") != -1)
                    zipcode = v.Substring("zip_code".Length+3).Trim('"');
                if (v.IndexOf("latitude") != -1)
                    latitude = float.Parse(v.Substring("latitude".Length + 3).Trim('"'));
                if (v.IndexOf("longitude") != -1)
                    longitude = float.Parse(v.Substring("longitude".Length + 3).Trim('"'));
            }
                
            //Console.WriteLine(serviceResult);
        }
        public string ToString()
        {
            return city+","+state+","+zipcode;
        }
    }
}
