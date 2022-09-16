using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.User
{
    public class UserService
    {
        private readonly XNodeConfiguration _xNodeConfiguration;

        public UserService(XNodeConfiguration xNodeConfiguration)
        {
            _xNodeConfiguration = xNodeConfiguration;
        }

        public bool TryGetUserRole(string userName, string password, out string role)
        {
            role = null;

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{_xNodeConfiguration.ServiceUrl}/api/v3/node/users/role";
                client.AddBasicAuthorizationHeader(userName, password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    role = (content);
                    return true;
                }
            }
            catch (System.Exception)
            {

            }

            return false;
        }

    }
}
