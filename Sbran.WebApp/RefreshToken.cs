using System;
using System.Text.Json.Serialization;

namespace Sbran.WebApp
{
    public class RefreshToken
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }    // can be used for usage tracking
        // can optionally include other metadata, such as user agent, ip address, device name, and so on

        [JsonPropertyName("tokenString")]
        public string TokenString { get; set; }

        [JsonPropertyName("expire_in")]
        public DateTime ExpireAt { get; set; }
    }
}
