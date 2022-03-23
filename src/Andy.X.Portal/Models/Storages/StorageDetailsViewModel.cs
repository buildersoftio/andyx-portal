using System;
using System.Collections.Concurrent;

namespace Andy.X.Portal.Models.Storages
{
    public class StorageDetailsViewModel
    {
        public Guid StorageId { get; set; }
        public string StorageName { get; set; }
        public StorageStatus StorageStatus { get; set; }
        public ConcurrentDictionary<string, Agent> Agents { get; set; }
        public int ActiveAgentIndex { get; set; }


        public bool IsLoadBalanced { get; set; }

        public int AgentMaxNumber { get; set; }
        public int AgentMinNumber { get; set; }


        public StorageDetailsViewModel()
        {
            Agents = new ConcurrentDictionary<string, Agent>();
        }
    }

    public enum StorageStatus
    {
        Active = 1,
        Inactive = 2,
        Blocked = 3
    }

    public class Agent
    {
        public string ConnectionId { get; set; }
        public Guid AgentId { get; set; }

        // Think! I don't know if we need AgentName
        public string AgentName { get; set; }

        public Agent()
        {
            AgentId = Guid.NewGuid();
        }
    }
}
