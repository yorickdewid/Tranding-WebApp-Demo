using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade;
using Trade.Models;
using System.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;

namespace APIPoller
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Groep 8 API poller");
            DBCreate db = new DBCreate();
            string APIurl = ConfigurationManager.AppSettings["APIurl"];
            string App_id = ConfigurationManager.AppSettings["App_id"];

            WebClient c = new WebClient();
            var data = (new WebClient()).DownloadString("https://" + APIurl + "?app_id=" + App_id);
            var CurrencyList = (JObject.Parse(data)).Last.First.ToList();
            foreach(var Currency in CurrencyList)
            {
                Forex f = new Forex();
                f.Code = ((Newtonsoft.Json.Linq.JProperty)Currency).Name.ToString();
                f.Ratio = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)Currency).Value.ToString());

                AddNewForex(db, f);
            }
        }

        private static void AddNewForex(DBCreate db, Forex forex)
        {
            db.Forexes.Add(forex);
            db.SaveChanges();
        }
    }
}
