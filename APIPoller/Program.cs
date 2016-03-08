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
        /// <summary>
        /// Clear the database, retrieve the data and add this to the database.
        /// This is meant to be done hourly.
        /// </summary>
        /// <param name="args">none</param>
        static void Main(string[] args)
        {
            Console.Write("Groep 8 API poller\n");
            
            string APIurl = ConfigurationManager.AppSettings["APIurl"];
            string App_id = ConfigurationManager.AppSettings["App_id"];

            WebClient c = new WebClient();
            var data = (new WebClient()).DownloadString("https://" + APIurl + "?app_id=" + App_id);
            var CurrencyList = (JObject.Parse(data)).Last.First.ToList();

            DBCreate db = new DBCreate();
            ClearAllData(db);
            Console.WriteLine("Starting the import... ");
            foreach (var Currency in CurrencyList)
            {
                Forex f = new Forex();
                f.Code = ((Newtonsoft.Json.Linq.JProperty)Currency).Name.ToString();
                f.Ratio = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)Currency).Value.ToString());

                AddNewForex(db, f);
            }
            Console.WriteLine("[Done]");
            Console.Write("Saving the database... ");
            db.SaveChanges();
            Console.WriteLine("[Done]");
           
        }

        /// <summary>
        /// Removes everything in the Forex table
        /// </summary>
        /// <param name="db">DBCreate object to connect to database</param>
        public static void ClearAllData(DBCreate db)
        {
            Console.Write("Clearing the database... ");
            db.Forexes.RemoveRange(db.Forexes);
            Console.WriteLine("[Done]");
        }

        /// <summary>
        /// Add a Forex object
        /// </summary>
        /// <param name="db">DBCreate object to connect to database</param>
        /// <param name="forex">The Forex object to add</param>
        private static void AddNewForex(DBCreate db, Forex forex)
        {
            Console.WriteLine("    Importing: [" +forex.Code + " => " + forex.Ratio + "]");
            db.Forexes.Add(forex);
        }
    }
}
