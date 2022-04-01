using System;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Consumers
{
    public class ConsumerDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string Topic { get; set; }

        public List<string> Connections { get; set; }
        public List<string> ExternalConnections { get; set; }

        // This property is used to send to the next shared consumer. (This property will replace the random)
        public int CurrentConnectionIndex { get; set; }
        public bool IsLocal { get; set; }

        public Guid Id { get; set; }
        public string ConsumerName { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        public ConsumerSettings ConsumerSettings { get; set; }

        public ConsumerDetailsViewModel()
        {
            Connections = new List<string>();
            ExternalConnections = new List<string>();
            ConsumerSettings = new ConsumerSettings();

            // is local -> flag if consumer is conencted to this node
            IsLocal = true;
        }
    }
    public class ConsumerSettings
    {
        public InitialPosition InitialPosition { get; set; }
        public ConsumerSettings()
        {
            InitialPosition = InitialPosition.Latest;
        }
    }

    public enum SubscriptionType
    {
        /// <summary>
        /// Only one reader
        /// </summary>
        Exclusive,
        /// <summary>
        /// One reader with one backup
        /// </summary>
        Failover,
        /// <summary>
        /// Shared to more than one reader.
        /// </summary>
        Shared
    }

    public enum InitialPosition
    {
        Earliest,
        Latest
    }
}
