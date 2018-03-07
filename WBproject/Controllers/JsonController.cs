using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;
using System.Net.Http;
using WBproject.Models;
using Json;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WBproject.Controllers
{   [Route("Json")]
    public class JsonController : Controller
    {
        public double EPSILON = 0.00001;

        [HttpGet]
        public HttpResponseMessage Json()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet("$lat={lat}$lng={lng}")]
        public JArray Map(double lat, double lng)
        {
            int radius = 250;
            int limit = 5;
            if (Math.Abs(lat - 47.246748) < EPSILON && Math.Abs(lng - -122.440328) < EPSILON) ViewData["GPS"] = false;

            Location loc = new Location(lng, lat);
            Events events = new Events(loc, radius, limit);

            IList<Point> locList = new List<Point>();
            IList<string> eventList = new List<string>();
            foreach (var c in events.list)
            {
                locList.Add(new Point() { lng = c.loc.lng, lat = c.loc.lat });
                eventList.Add(c.Des);

            }
            var respond = new HttpResponseMessage(HttpStatusCode.OK);
            JArray array = new JArray();

            for (int i = 0; i < locList.Count;i++)
            {
                JObject o = new JObject();
                o[locList[i].ToString()] = eventList[i];
                array.Add(o);
            }

            return array;

        }
    }
}
