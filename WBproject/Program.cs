

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Json;
using SODA;

namespace WBproject
{
    public class Program
    {
  
        private static void Main(string[] args)
        {
            Location locate = new Location();
            locate.get();
            Console.Write(locate.ToString());
            Console.WriteLine("({0},{1})", locate.longitude, locate.latitude);

            //locate.latitude = (float)47.244615;
            //locate.longitude = (float)-122.4380899;
            Crimes crime = new Crimes();
            crime.get(locate,100);
            foreach(var v in  crime.list)
            {
                Console.WriteLine(v);

            }
            FireIncidents fire = new FireIncidents();
            fire.get(locate,100);
            foreach (var v in fire.list)
            {
                Console.WriteLine(v);
            }


        }
    }
}
