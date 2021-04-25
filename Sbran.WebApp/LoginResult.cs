using Newtonsoft.Json;
using System;

namespace Sbran.WebApp
{
    public sealed class LoginResult
    {
        [JsonProperty("username")]
        public string? UserName { get; init; }

        [JsonProperty("role")]
        public string? Role { get; init; }

        [JsonProperty("originalUserName")]
        public string? OriginalUserName { get; init; }

        [JsonProperty("access_token")]
        public string? AccessToken { get; init; }

        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; init; }

        [JsonProperty("expires_in")]
        public DateTime? ExpiresAt { get; init; }
    }
}