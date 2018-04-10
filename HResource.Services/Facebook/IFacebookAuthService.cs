using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HResource.Objects.Facebook;

namespace HResource.Services.Facebook
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenResponse> GetAccessToken(string code);
        Task<FacebookValidationResponse> ValidateAccessToken(string accessToken);
        Task<FacebookUserData> GetUserData(string accessToken);
    }
}
