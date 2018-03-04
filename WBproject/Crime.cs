using System;
using System.Collections;
using System.Collections.Generic;
using SODA;

namespace WBproject
{
    public class Crimes
    {
        public ArrayList list;
        public class Crime
        {
            public string intersection_address;
            public string date;
            public string time;
            public string crime;
            public Location loc;

            public Crime()
            {
                loc = new Location();
            }

            public override string ToString()
            {
                return "At " + intersection_address + ", " + crime + ", on " +
                    date + " " + time;
            }
        }

        public Crimes()
        {
            list = new ArrayList();

        }
        public void sorting()
        {
            
        }
        public void get(Location locate,int radius)
        {
            var client = new SodaClient("data.cityoftacoma.org", "faxxyxOUEBkwIxlgvMgFaEViQ");

            var dataset = client.GetResource<Dictionary<string, object>>("vzsr-722t");


            string sql = "within_circle(intersection," +
                locate.latitude.ToString() + "," +
                      locate.longitude.ToString() + ","+radius.ToString()+")";
            var soql = new SoqlQuery().Where(sql);

            var results = dataset.Query(soql);

            foreach (var row in results)
            {
                var tmp = new Crime();
                if (0 != row.Count)
                {
                    //foreach (var vv in row)
                        //Console.WriteLine(vv);
                    try
                    {
                        tmp.intersection_address = row["intersection_address"].ToString();
                        tmp.date = row["occurred_on"].ToString().Substring(0,10);
                        tmp.time = row["approximate_time"].ToString();
                        tmp.crime = row["crime"].ToString();

                        tmp.loc.city = row["intersection_city"].ToString();
                        tmp.loc.state = row["intersection_state"].ToString();
                        tmp.loc.zipcode = locate.zipcode;
                        //longthy
                        string intersection = row["intersection"].ToString();
                        String coordinates = intersection.Substring(intersection.IndexOf("coordinates") + 13).Trim('[', ']', '}', '{', '\n', '\r', ' ', '\t');
                        //Console.WriteLine(coordinates.Split(',')[1]);
                        tmp.loc.latitude = float.Parse(coordinates.Split(',')[1]);
                        tmp.loc.longitude = float.Parse(coordinates.Split(',')[0]);
                    }catch(Exception e){Console.WriteLine("Error occurred in Crime entrance"); }
                }
                list.Add(tmp);
            }
        }
    }
}
