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
            return Map(47.246748, -122.440114,250,5);

        }
        [Route("Home/Map/$lat={lat}$lng={lng}")]
        public IActionResult Map(double lat, double lng)
        {
            return Map(lat, lng, 250, 5);
        }
        [Route("Home/Map/$lat={lat}$lng={lng}$radius={radius}/{limit}")]
        public IActionResult Map(double lat, double lng, int radius, int limit)
        {
            ViewData["Message"] = "Events Map!";
            ViewData["lat"] = lat;
            ViewData["lng"] = lng;
            Location loc = new Location(lng, lat);
            Events events = new Events(loc, radius, limit);

            IList<Point> locList = new List<Point>();
            IList<string> eventList = new List<string>();
            foreach (var c in events.list)
            {
                locList.Add(new Point() { lng = c.loc.lng, lat = c.loc.lat });
                eventList.Add(c.Des);

            }


            ViewData["LocList"] = locList;
            ViewData["EventList"] = eventList;
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
