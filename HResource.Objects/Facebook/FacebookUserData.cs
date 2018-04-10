using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HResource.Objects.Facebook
{
    public class FacebookUserData
    {
        public long Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        public string Name { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public FacebookPictureData Picture { get; set; }
    }
}
