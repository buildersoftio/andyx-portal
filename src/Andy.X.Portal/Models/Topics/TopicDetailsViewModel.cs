using Andy.X.Portal.Models.Subscriptions;
using System;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Topics
{
    public class TopicDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        public TopicSettings TopicSettings { get; set; }
        public List<TopicSubscription> Subscriptions { get; set; }
        public List<string> Producers { get; set; }

        public TopicDetailsViewModel()
        {
            TopicSettings = new TopicSettings();
            Subscriptions = new List<TopicSubscription>();
            Producers = new List<string>();
        }
    }
    public class TopicSettings
    {
        public ulong WriteBufferSizeInBytes { get; set; }
        public int MaxWriteBufferNumber { get; set; }

        public int MaxWriteBufferSizeToMaintain { get; set; }
        public int MinWriteBufferNumberToMerge { get; set; }
        public int MaxBackgroundCompactionsThreads { get; set; }
        public int MaxBackgroundFlushesThreads { get; set; }
    }
}
