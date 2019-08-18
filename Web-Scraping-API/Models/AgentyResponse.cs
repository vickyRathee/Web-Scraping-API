using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingAPI.Models
{
    public class AddURLResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
    }

    public class JobStartResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public int job_id { get; set; }
    }

    public class JobStatusResponse
    {
        public int job_id { get; set; }
        public string agent_id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public int pages_total { get; set; }
        public int pages_processed { get; set; }
        public int pages_successed { get; set; }
        public int pages_failed { get; set; }
        public int pages_credit { get; set; }
        public DateTime created_at { get; set; }
        public DateTime started_at { get; set; }
        public DateTime? completed_at { get; set; }
        public DateTime? stopped_at { get; set; }
        public bool is_scheduled { get; set; }
        public string error { get; set; }
    }
    
    public class JobResultResponse
    {
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int returned { get; set; }
        public List<dynamic> result { get; set; }
    }
}
