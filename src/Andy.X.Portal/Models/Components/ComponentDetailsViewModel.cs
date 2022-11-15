using Andy.X.Portal.Models.Tenants;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Components
{
    public class ComponentDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        public List<string> Topics { get; set; }
        public List<ComponentRetention> ComponentRetentions { get; set; }
        public ComponentSettings ComponentSettings { get; set; }
        public List<Token> Tokens { get; set; }


        public ComponentDetailsViewModel()
        {
            Topics = new List<string>();
            ComponentRetentions = new List<ComponentRetention>();
            ComponentSettings = new ComponentSettings();
            Tokens = new List<Token>();
        }
    }


    public class ComponentRetention
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public RetentionType Type { get; set; }
        public long TimeToLiveInMinutes { get; set; }
    }

    public class ComponentSettings
    {
        public bool IsTopicAutomaticCreationAllowed { get; set; }
        public bool EnforceSchemaValidation { get; set; }
        public bool IsAuthorizationEnabled { get; set; }

        public bool IsSubscriptionAutomaticCreationAllowed { get; set; }
        public bool IsProducerAutomaticCreationAllowed { get; set; }
    }
}
