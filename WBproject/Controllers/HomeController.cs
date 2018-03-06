using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WBproject.Models;

namespace WBproject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Map()
        {
            ViewData["Message"] = "Events Map!";

            Crimes crime = new Crimes();
            Location loc = new Location(-122.440114, 47.246748);
            crime.get(loc, 250,5);
            IList<Point> locList = new List<Point>();
            IList<string> eventList = new List<string>();
            foreach(var c in crime.list)
            {
                locList.Add(new Point() { lng = c.loc.longitude.ToString(), lat = c.loc.latitude.ToString() });
                eventList.Add(c.crime);
                Console.WriteLine(c.crime);
            }

            Console.WriteLine("!!&!*@#!((((((requested");

            ViewData["crimeLocList"] = locList;
            ViewData["crimeEventList"] = eventList;
            return View();

        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
