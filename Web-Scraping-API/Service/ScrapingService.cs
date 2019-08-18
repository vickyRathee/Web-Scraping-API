using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapingAPI.Models;

namespace WebScrapingAPI.Service
{
    class ScrapingService
    {
        public static RestClient client = new RestClient("https://api.agenty.com/v1");

        public AddURLResponse AddUrlsToAgent(string agentId, List<string> urls)
        {
            var request = new RestRequest($"/inputs/{agentId}?apikey={Program.AgentyApiKey}", 
                Method.PUT);
            var requestBody = new { type = "MANUAL", data = urls.ToArray() };

            request.AddJsonBody(requestBody);
            request.AddHeader("Content-type", "application/json");

            var response = client.Execute<AddURLResponse>(request);
            return response.Data;
        }

        public JobStartResponse StartScrapingAgent(string agentId)
        {
            var request = new RestRequest($"/jobs/scraping/async?apikey={Program.AgentyApiKey}",
                Method.POST);
            request.AddHeader("Content-type", "application/json");

            var requestBody = new { agent_id = agentId };
            request.AddJsonBody(requestBody);

            var response = client.Execute<JobStartResponse>(request);
            return response.Data;
        }

        public JobStatusResponse GetJobStatus(int jobId)
        {
            var request = new RestRequest($"/jobs/{jobId}?apikey={Program.AgentyApiKey}",
                Method.GET);
            request.AddHeader("Content-type", "application/json");

            var response = client.Execute<JobStatusResponse>(request);
            return response.Data;
        }

        public JobResultResponse GetJobResult(int jobId)
        {
            var request = new RestRequest($"/jobs/{jobId}/result?apikey={Program.AgentyApiKey}",
                Method.GET);
            request.AddHeader("Content-type", "application/json");

            var response = client.Execute<JobResultResponse>(request);
            return response.Data;
        }
    }
}
