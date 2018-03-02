

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
                locate.get_Location();
            Console.Write(locate.ToString());
            Console.WriteLine("({0},{1})",locate.longitude,locate.latitude);



            //initialize a new client
            //make sure you register for your own app token (http://dev.socrata.com/register)
            var client = new SodaClient("data.cityoftacoma.org", "faxxyxOUEBkwIxlgvMgFaEViQ");

            //read metadata of a dataset using the resource identifier (Socrata 4x4)
            var metadata = client.GetMetadata("iww5-t4fx");
            Console.WriteLine("{0} has {1} views.", metadata.Name, metadata.ViewsCount);

            //get a reference to the resource itself
            //the result (a Resouce object) is a generic type
            //the type parameter represents the underlying rows of the resource
            var dataset = client.GetResource<Dictionary<string, object>>("iww5-t4fx");

            //collections of an arbitrary type can be returned
            //using SoQL and a fluent query building syntax
            string sql="within_circle(location,"+locate.latitude.ToString()+","+locate.longitude.ToString()+",150)";
            var soql = new SoqlQuery().Where(sql);

            var results = dataset.Query(soql);
           
            foreach(var row in results)
            {
                if(0!=row.Count())
                    foreach(var col in row)
                        Console.Write(col);
                Console.WriteLine();
            }

        }
    }
}
