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
        public decimal payment { get; set; }
        public DateTime date { get; set; }
        public long account_number { get; set; }
        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class Transcation
    {
        public string city { get; set; }
        public List<Service> services { get; set; }
        public decimal total { get; set; }

        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class Service
    {
        public string name { get; set; }
        public List<Payer> payers { get; set; }
        public decimal total { get; set; }
    }
}
