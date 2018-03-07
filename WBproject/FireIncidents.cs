using System;
using SODA;

using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System.Collections.Generic;

namespace WBproject
{
    public class FireIncidents
    {
        public ArrayList<Fire> list;
        public class Fire
        {
            
            public string intersection_address;
            public string fire_generalcause;
            public string firetype;
            public string date;
            public string time1;
            public string time2;
            public string estimatedtotalfireloss;
            public Location loc;

            public override string ToString()
            {
                return "At " + intersection_address +", " + 
                    firetype + " caused by " + fire_generalcause+" occured on "+
                    date + " " + time1+"~"+time2+". estimated loss:"+estimatedtotalfireloss;
            }

            public Fire()
            {
                loc= new Location();
            }
        }
        public FireIncidents()
        {
            list = new ArrayList<Fire>();

        }
        public void get(Location locate, int radius)
        {
            get(locate, radius, 999);
        }
        public void get(Location locate,int radius,int limit)
        {

            //initialize a new client
            //make sure you register for your own app token (http://dev.socrata.com/register)
            var client = new SodaClient("data.cityoftacoma.org", "faxxyxOUEBkwIxlgvMgFaEViQ");

            //read metadata of a dataset using the resource identifier (Socrata 4x4)
            //var metadata = client.GetMetadata("iww5-t4fx");
            //Console.WriteLine("{0} has {1} views.", metadata.Name, metadata.ViewsCount);

            //get a reference to the resource itself
            //the result (a Resouce object) is a generic type
            //the type parameter represents the underlying rows of the resource
            var dataset = client.GetResource<Dictionary<string, object>>("iww5-t4fx");

            //collections of an arbitrary type can be returned
            //using SoQL and a fluent query building syntax
            string sql = "within_circle(location," + 
                locate.latitude.ToString() + "," + 
                      locate.longitude.ToString() + ","+radius.ToString()+")";
            var soql = new SoqlQuery().Where(sql).Limit(limit);

            var results = dataset.Query(soql);

            foreach (var row in results)
            {
                var tmp = new Fire();
                if (0 != row.Count){
                    //foreach (var vv in row)
                        //Console.WriteLine(vv);
                    try
                    {
                        tmp.intersection_address = row["location_address"].ToString();
                        tmp.fire_generalcause = row["fire_generalcause"].ToString();
                        tmp.firetype = row["firetype"].ToString();
                        tmp.time1 = row["firstunitturnout"].ToString();
                        tmp.time2 = row["incidentclosed"].ToString();
                        tmp.date = row["incidentdate"].ToString().Substring(0, 10);
                        tmp.estimatedtotalfireloss = row["estimatedtotalfireloss"].ToString();
                        tmp.loc.city = row["city"].ToString();
                        tmp.loc.state = row["state"].ToString();
                        tmp.loc.zipcode = row["zipcode"].ToString();
                        tmp.loc.latitude = float.Parse(row["latitude"].ToString());
                        tmp.loc.longitude = float.Parse(row["longitude"].ToString());
                    }catch(Exception e)
                    {Console.WriteLine("Error occurred in FireIncident entrance");}
                }
                list.Add(tmp);
            }
        }
    }
}
