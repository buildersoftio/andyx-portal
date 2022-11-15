namespace Andy.X.Portal.Models.Home
{
    public class HomeViewModel
    {
        public long TenantCount { get; set; }
        public long ProductCount { get; set; }
        public long ComponentCount { get; set; }
        public long TopicCount { get; set; }

        public long ProducersCount { get; set; }
        public long SubscriptionCount { get; set; }

        public string NodeUrl { get; set; }
        public string Username { get; set; }
        public HomeViewModel()
        {
            TenantCount = 0;
            ProductCount = 0;
            ComponentCount = 0;
            TopicCount = 0;

            ProducersCount = 0;
            SubscriptionCount = 0;
        }
    }
}
