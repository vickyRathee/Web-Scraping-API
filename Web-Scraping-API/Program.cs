using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp; // Install-Package RestSharp
using WebScrapingAPI.Service;

namespace WebScrapingAPI
{
    class Program
    {
        // If you don't have one - Signup https://www.agenty.com/
        public static string AgentyApiKey = "Your api key here";
        
        static void Main(string[] args)
        {
            string agentId = "qgddevdqzm";

            var agentyService = new ScrapingService();

            List<string> urls = new List<string>
            {
                "http://books.toscrape.com/catalogue/set-me-free_988/index.html",
                "http://books.toscrape.com/catalogue/starving-hearts-triangular-trade-trilogy-1_990/index.html",
                "http://books.toscrape.com/catalogue/the-black-maria_991/index.html"
            };
            Console.WriteLine("************Starting Agenty Service************");

            // Add URLs to Agent
            Console.WriteLine("****Add URLs to Agent****");
            var response1 = agentyService.AddUrlsToAgent(agentId, urls);
            Console.WriteLine($"StatusCode : {response1.status_code}");
            Console.WriteLine($"Message : {response1.message}");

            // Start the Scraping Job
            Console.WriteLine("****Start the Scraping Job****");
            var response2 = agentyService.StartScrapingAgent(agentId);
            Console.WriteLine($"StatusCode : {response2.status_code}");
            Console.WriteLine($"Message : {response2.message}");
            Console.WriteLine($"Job Id : {response2.job_id}");

            // Check job status in while Loop until its' completed/stopped/aborted
            Console.WriteLine("****Check job status in loop****");

            string[] completedStatus = new string[] { "completed", "stopped", "aborted" };
            string jobStatus = "running";

            while(!completedStatus.Any(x => x == jobStatus))
            {
                var response3 = agentyService.GetJobStatus(response2.job_id);
                jobStatus = response3.status;

                Console.WriteLine($"Job status : {response3.status} - Pages processed: ({response3.pages_processed}/{response3.pages_total})");
                if(!completedStatus.Any(x => x == jobStatus))
                {
                    Console.WriteLine($"Rechecking status after 2 second...");
                    Thread.Sleep(2000);
                }
            }

            Console.WriteLine("****Download scraping job result****");
            var response4 = agentyService.GetJobResult(response2.job_id);

            Console.WriteLine($"Total rows in result : {response4.total}");

            string localPath = @"Y:\scraping\result.txt";
            var json = JsonConvert.SerializeObject(response4.result);
            DataTable table = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            WriteToFile(table, localPath);

            Console.WriteLine($"Result download at: {localPath}");

            Console.ReadKey();
        }

        public static void WriteToFile(DataTable table, string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!File.Exists(filePath))
            {
                stringBuilder.AppendLine(string.Join("\t", table.Columns.Cast<DataColumn>().Select(arg => arg.ColumnName)));
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    stringBuilder.AppendLine(string.Join("\t", dataRow.ItemArray.Select(arg => arg.ToString())));
                }
                sw.Write(stringBuilder.ToString());
            }
        }

    }
}
