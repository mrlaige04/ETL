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
        public List<TranscationModel> transcations { get; set; } = new();

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

    public class TranscationModel
    {
        public string city { get; set; }
        public List<Service> services { get; set; } = new List<Service>();
        public decimal total { get; set; }

        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class TransactionDTO
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string city { get; set; }
        public decimal payment { get; set; }
        public DateTime date { get; set; }
        public long account_number { get; set; }
        public string service { get; set; }

        public string ConvertToJSON()
        {
            return JsonSerializer.Serialize(this);
        }
        
    }

    public class Service
    {
        public string name { get; set; }
        public List<Payer> payers { get; set; } = new List<Payer>();
        public decimal total { get; set; }
    }
}
