using Newtonsoft.Json;

namespace BookBarter.API.Common.Models
{
    public class ErrorDetails
    {
        public string[] Messages { get; set; } = default!;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}