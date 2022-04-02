using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Components
{
    public class ComponentDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ComponentSettings Settings { get; set; }
        public ConcurrentDictionary<string, TopicInComponent> Topics { get; set; }

        public ComponentDetailsViewModel()
        {
            Settings = new ComponentSettings();
            Topics = new ConcurrentDictionary<string, TopicInComponent>();
        }
    }

    public class TopicInComponent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ComponentSettings
    {
        public bool AllowSchemaValidation { get; set; }
        public bool AllowTopicCreation { get; set; }

        public bool EnableAuthorization { get; set; }
        public List<ComponentToken> Tokens { get; set; }

        public ComponentRetention RetentionPolicy { get; set; }



        public ComponentSettings()
        {
            AllowSchemaValidation = false;
            AllowTopicCreation = true;
            EnableAuthorization = false;

            Tokens = new List<ComponentToken>();
            RetentionPolicy = new ComponentRetention();
        }
    }

    public class ComponentRetention
    {
        public string Name { get; set; }
        public long RetentionTimeInMinutes { get; set; }

        public ComponentRetention()
        {
            Name = "default";
            RetentionTimeInMinutes = -1;
        }
    }

    public class ComponentToken
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // TOKEN will be generated from andyx-cli and it be added manually via tenants.json
        public string Token { get; set; }

        public bool IsActive { get; set; }

        public bool CanConsume { get; set; }
        public bool CanProduce { get; set; }

        public DateTime ExpireDate { get; set; }
        public string IssuedFor { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
