using Newtonsoft.Json;

namespace BookBarter.API.Common.Models
{
    public class ErrorDetails
    {
        public string[] Messages { get; set; } = default!;  // FIX: List<string> is better

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}