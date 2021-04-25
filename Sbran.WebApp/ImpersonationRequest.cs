using System.Text.Json.Serialization;

namespace Sbran.WebApp
{
    public class ImpersonationRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}