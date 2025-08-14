using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class PaymeRequestParams
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("time")]
        public long Time { get; set; } = 0;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; } = 0;

        [JsonPropertyName("from")]
        public long From { get; set; } = 0;
        [JsonPropertyName("to")]
        public long To { get; set; } = 0;

        [JsonPropertyName("reason")]
        public int Reason { get; set; } = 0;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("account")]
        public Account Account { get; set; }
    }
}
