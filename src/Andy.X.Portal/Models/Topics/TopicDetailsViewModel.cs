using System;

namespace Andy.X.Portal.Models.Topics
{
    public class TopicDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public TopicSettings TopicSettings { get; set; }

        public TopicDetailsViewModel()
        {
            TopicSettings = new TopicSettings();
        }
    }
    public class TopicSettings
    {
        public bool IsPersistent { get; set; }

        public TopicSettings()
        {
            IsPersistent = true;
        }
    }
}
