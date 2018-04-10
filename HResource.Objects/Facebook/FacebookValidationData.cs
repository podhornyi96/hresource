using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HResource.Objects.Facebook
{
    public class FacebookValidationData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("application")]
        public string Application { get; set; }
        [JsonProperty("expires_at")]
        public int ExpiresAt { get; set; }
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("issued_at")]
        public int IssuedAt { get; set; }
        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
