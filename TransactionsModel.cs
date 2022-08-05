using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETL
{
    public class Output
    {
        public List<Transcation> transcations { get; set; }

        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(transcations);
        }
    }
    public class Payer
    {
        public string name { get; set; }
        public string payment { get; set; }
        public string date { get; set; }
        public string account_number { get; set; }
    }

    public class Transcation
    {
        public string city { get; set; }
        public List<Service> services { get; set; }
        public string total { get; set; }
    }

    public class Service
    {
        public string name { get; set; }
        public List<Payer> payers { get; set; }
        public string total { get; set; }
    }
}
