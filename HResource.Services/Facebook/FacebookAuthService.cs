using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HResource.Objects.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HResource.Services.Facebook
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly IConfiguration _configuration;

        public FacebookAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<FacebookTokenResponse> GetAccessToken(string code)
        {
            using (var client = new HttpClient())
            {
                var tokenDataResponse = await client.GetStringAsync(
                    "https://graph.facebook.com/v2.12/oauth/access_token?" +
                    $"client_id={_configuration["Authentication:Facebook:AppId"]}&" +
                    $"redirect_uri={_configuration["Authentication:Facebook:RedirectUri"]}&" +
                    $"client_secret={_configuration["Authentication:Facebook:AppSecret"]}&" +
                    $"code={code}");

                return JsonConvert.DeserializeObject<FacebookTokenResponse>(tokenDataResponse);
            }
        }

        public async Task<FacebookValidationResponse> ValidateAccessToken(string accessToken)
        {
            using (var client = new HttpClient())
            {
                var tokenValidationResponse = await client.GetStringAsync(
                    $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={accessToken}");

                return JsonConvert.DeserializeObject<FacebookValidationResponse>(tokenValidationResponse);
            }
        }

        public async Task<FacebookUserData> GetUserData(string accessToken)
        {
            using (var client = new HttpClient())
            {
                var userDataResponse = await client.GetStringAsync(
                    $"https://graph.facebook.com/v2.12/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={accessToken}");

                return JsonConvert.DeserializeObject<FacebookUserData>(userDataResponse);
            }
        }

    }
}
