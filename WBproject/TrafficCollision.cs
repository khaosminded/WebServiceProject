using System;
using SODA;
using System.Collections.Generic;

using WBproject.Models;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace WBproject
{
    public class TrafficCollision
    {
        public ArrayList<Collision> list;
        public class Collision
        {

            public string most_severe_injury_type{ get; set; }
            public string date{ get; set; }
            public string weather{ get; set; }
            public string lighting_conditions{ get; set; }
            public string jurisdiction{ get; set; }
            public Location coordinate = new Location();
            public string place;
            public override string ToString()
            {
                return string.Format("At {5}, {0} caused by a traffic collision under {2} & {3} condition, on {1}. jurisdiction:{4}", most_severe_injury_type, date, weather, lighting_conditions, jurisdiction,coordinate.reverseGeocoding());
            }

        }

		
        public TrafficCollision()
        {
            list = new ArrayList<Collision>();
        }
        public void get(Location locate, int radius)
        {
            get(locate, radius, 999);
        }
        public void get(Location locate,int radius,int limit)
        {
            //https://data.cityoftacoma.org/resource/kjk6-j7c9.json
            var client = new SodaClient("data.cityoftacoma.org", "faxxyxOUEBkwIxlgvMgFaEViQ");

            var dataset = client.GetResource<Dictionary<string, object>>("kjk6-j7c9");
            string sql = "within_circle(collision_location," +
                locate.latitude.ToString() + "," +
                      locate.longitude.ToString() + "," + radius.ToString() + ")";
            var soql = new SoqlQuery().Where(sql).Limit(limit);
            var results = dataset.Query(soql);
            foreach (var row in results)
            {

                if (0 != row.Count)
                try
                {

                        Collision collision = new Collision
                        {
                        
                    };
                    try { collision.most_severe_injury_type = row["most_severe_injury_type"].ToString(); } 
                    catch (Exception e) { }
                    try { collision.lighting_conditions = row["lighting_conditions"].ToString(); }
                        catch (Exception e) { }
                    try { collision.jurisdiction = row["jurisdiction"].ToString(); }
                        catch (Exception e) { }
                    try { collision.weather = row["weather"].ToString(); }
                        catch (Exception e) { }
                    try { collision.date = row["date"].ToString(); }catch(Exception e){}

                    string collision_location = row["collision_location"].ToString();
                    string coordinates = collision_location.Substring(collision_location.IndexOf("coordinates") + 13).Trim('[', ']', '}', '{', '\n', '\r', ' ', '\t');
                    //Console.WriteLine(float.Parse(coordinates.Split(',')[0]));

                    collision.coordinate.latitude = float.Parse(coordinates.Split(',')[1]);
                    collision.coordinate.longitude = float.Parse(coordinates.Split(',')[0]);
                        collision.place = collision.coordinate.reverseGeocoding();
                    list.Add(collision);

                }
                catch (Exception e) { 
                    //Console.WriteLine("Error traffic data entrance"); 

                    //https://data.cityofchicago.org/resource/6zsd-86xi.json?
                        //$where=date between '2018-02-27T12:00:00' and '2018-03-04T14:00:00'
                }

            }

        }


    }
}
