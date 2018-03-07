

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SODA;

namespace WBproject
{
    public class Program
    {
  
        private static void Main(string[] args)
        {
            //Test t = new Test();




            BuildWebHost(args).Run();

        }
        public static void getAllEvents()
        {
            Location locate = new Location();
            locate.get();
            int radius = 250;

            Console.Write(locate.ToString());
            Console.WriteLine("({0},{1})", locate.longitude, locate.latitude);
            Console.Write(locate.reverseGeocoding());

            //locate.latitude = (float)47.244615;
            //locate.longitude = (float)-122.4380899;




            TrafficCollision collisions = new TrafficCollision();
            collisions.get(locate,radius);
            foreach(var v in  collisions.list)
            {
                Console.WriteLine(v);
            }
            Crimes crime = new Crimes();
            crime.get(locate,radius);
            foreach(var v in  crime.list)
            {
                Console.WriteLine(v);

            }
            FireIncidents fire = new FireIncidents();
            fire.get(locate,radius);
            foreach (var v in fire.list)
            {
                Console.WriteLine(v);
            }
        }
        public static IWebHost BuildWebHost(string[] args) =>             WebHost.CreateDefaultBuilder(args)
                   .UseUrls("https://*:8080/")                 .UseStartup<Startup>()                 .Build(); 
    }
}
