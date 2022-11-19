using System.Collections.Generic;

namespace Andy.X.Portal.Models.Subscriptions
{
    public class SubscriptionListViewModel
    {
        public List<SubscriptionActivity> Subscriptions { get; set; }

        public SubscriptionListViewModel()
        {
            Subscriptions = new List<SubscriptionActivity>();
        }
    }

    public class TopicSubscription
    {
        public string Name { get; set; }
        public SubscriptionType Type { get; set; }
    }

    public class SubscriptionActivity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string Topic { get; set; }


    }
}
