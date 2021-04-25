using System.Text.Json.Serialization;

namespace Sbran.WebApp
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}