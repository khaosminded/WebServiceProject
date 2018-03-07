using System;
using System.Collections;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace WBproject.Models
{
    public class Events
    {
        public ArrayList<Event> list;

        public class Event
        {
            public Point loc;
            public string Des;
            public Event(Point a, string b)
            {
                loc = a;
                Des = b;
            }

        }
        public Events(Location loc,int radius,int limit)
        {
            list = new ArrayList<Event>();
            Crimes crimes = new Crimes();
            TrafficCollision trafficCollision = new TrafficCollision();
            FireIncidents fireIncidents = new FireIncidents();
            crimes.get(loc,radius,limit);
            trafficCollision.get(loc, radius, limit);
            fireIncidents.get(loc, radius, limit);
            for (int i = 1; i < crimes.list.Length;i++)
            {
                Point point = new Point() { lng =crimes.list[i].loc.longitude.ToString(),
                    lat= crimes.list[i].loc.latitude.ToString()};
                Event tmp = new Event(point,crimes.list[i].ToString());
                this.list.Add(tmp);
            }
            for (int i = 1; i < trafficCollision.list.Length; i++)
            {
                Point point = new Point()
                {
                    lng = trafficCollision.list[i].coordinate.longitude.ToString(),
                    lat = trafficCollision.list[i].coordinate.latitude.ToString()
                };
                Event tmp = new Event(point, trafficCollision.list[i].ToString());
                this.list.Add(tmp);
            }
            for (int i = 1; i < fireIncidents.list.Length; i++)
            {
                Point point = new Point()
                {
                    lng = fireIncidents.list[i].loc.longitude.ToString(),
                    lat = fireIncidents.list[i].loc.latitude.ToString()
                };
                Event tmp = new Event(point, fireIncidents.list[i].ToString());
                this.list.Add(tmp);
            }

        }
    }
}
