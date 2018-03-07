using System;
using System.Collections;

namespace WBproject.Models
{
    public class Point
    {

        public string lng { get; set; }
        public string lat { get; set; }
        public string ToString()
        {
            return "(" + lat + "," + lng + ")";
        }
    }
}
