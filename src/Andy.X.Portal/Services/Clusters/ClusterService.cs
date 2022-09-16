using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models;
using Andy.X.Portal.Models.Clusters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace Andy.X.Portal.Services.Clusters
{
    public class ClusterService
    {
        private readonly ILogger<ClusterService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;
        public ClusterService(ILogger<ClusterService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public ClustersDetailsViewModel GetClustersDetailsViewModel(Models.User user)
        {
            var clustersDetailsViewModel = new ClustersDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/clusters";
            client.AddBasicAuthorizationHeader(user.Username, user.Username);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                clustersDetailsViewModel = JsonConvert.DeserializeObject<ClustersDetailsViewModel>(content);
            }

            return clustersDetailsViewModel;
        }
    }
}
