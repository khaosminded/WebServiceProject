using System;
using System.Collections.Generic;
using SODA;

namespace WBproject
{
    public class Test
    {
        public Test()
        {
            //query
            //https://data.cityoftacoma.org/resource/vzsr-722t.json?$where=occurred_on%20between%20%272018-01-10T12:00:00%27%20and%20%272018-03-02T14:00:00%27
            var client = new SodaClient("soda.demo.socrata.com", "faxxyxOUEBkwIxlgvMgFaEViQ");

            var dataset = client.GetResource<Dictionary<string, object>>("4tka-6guv");



            var soql = new SoqlQuery().Select("earthquake_id")
                                      .Where("magnitude > 3.0&source=pr");
            var results = dataset.Query(soql);
            foreach(var v in results)
            {
                foreach(var vv in v)
                {
                    Console.Write(vv);
                }
                Console.WriteLine();
            }
        }
    }
}
