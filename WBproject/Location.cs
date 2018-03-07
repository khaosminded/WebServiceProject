using System;
using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json.Linq;

namespace WBproject
{
    public class Location
    {
        public string steet;
        public string city;
        public string state;
        public float longitude;
        public float latitude;
        public string zipcode;

        public string formatAddr;
        public Location()
        {

        }

        public Location(double lng,double lat)
        {
            longitude = (float)lng;
            latitude = (float)lat;
        }
        public void setLocation(string street,string city, string state, float longitude, float latitude, string zipcode)
        {
            this.steet = street;
            this.city = city;
            this.state = state;
            this.longitude = longitude;
            this.latitude = latitude;
            this.zipcode = zipcode;
        }



        public void get()
        {
            getByIP(null);
        }
        public void getByIP(IPAddress ip)
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

        public override string ToString()
        {
            return city+","+state+","+zipcode;
        }
        public double getDistance(Location A, Location B)
        {
            double result;
            result = Math.Pow(A.latitude - B.latitude, 2) +
                         Math.Pow(A.longitude - B.longitude, 2);
            result = Math.Sqrt(result);
            return result;
        }
        public string  reverseGeocoding()
        {
            //https://maps.googleapis.com/maps/api/geocode/json?latlng
            //=40.714224,-73.961452&key=YOUR_API_KEY
            var serviceURL = "https://maps.googleapis.com/maps/api/geocode/json?latlng="+
                this.latitude+","+this.longitude+
                    "&result_type=street_address"+
                    "&key=AIzaSyAeKNcZyUlkSoXS4KRiB3JxRkJYLst1Ef0";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceURL);
            request.Method = "GET";
            request.ContentType = "text/xml; charset=utf-8";
            request.ContentLength = 0;

            HttpWebResponse serviceResponse = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);
            string json = readStream.ReadToEnd();
            JObject jo = JObject.Parse(json);


            formatAddr = (string)jo["results"][0]["formatted_address"];
            return formatAddr; 

        }
        public void Geocoding()
        {
            //google api key
            //AIzaSyAeKNcZyUlkSoXS4KRiB3JxRkJYLst1Ef0
        }
    }
}
