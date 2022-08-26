using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Clusters
{
    public class ClustersDetailsViewModel
    {
        // in-memory properties
        public long InThroughputInMB { get; set; }
        public long OutThroughputInMB { get; set; }
        public long ActiveDataIngestions { get; set; }
        public long ActiveSubscriptions { get; set; }


        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DistributionTypes ShardDistributionType { get; set; }
        public ClusterStatus Status { get; set; }

        public List<Shard> Shards { get; set; }

        public ClustersDetailsViewModel()
        {
            ShardDistributionType = DistributionTypes.Sync;
            Shards = new List<Shard>();
            Status = ClusterStatus.Starting;
        }
    }
    public enum DistributionTypes
    {
        Sync,
        Async,
    }

    public enum ClusterStatus
    {
        Online,
        Starting,
        Offline,
        Restarting,
        Recovering,
        Disconnecting,
    }

    public class Shard
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DistributionTypes ReplicaDistributionType { get; set; }
        public List<Replica> Replicas { get; set; }

        public Shard()
        {
            ReplicaDistributionType = DistributionTypes.Async;
            Replicas = new List<Replica>();
        }
    }

    public class Replica
    {
        public string NodeId { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NodeConnectionType ConnectionType { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ReplicaTypes Type { get; set; }


        public Replica()
        {
            NodeId = "standalone_01";
            Type = ReplicaTypes.MainOrWorker;
            ConnectionType = NodeConnectionType.NON_SSL;
        }
    }

    public enum ReplicaTypes
    {
        Main,
        Worker,

        // when MainOrWorker is set, one of the Nodes will produce an event to other nodes to tell that who is the main node.
        MainOrWorker,

        // In this replica no producer or consumer can connect when the other nodes are working (its perfect for DRC)
        BackupReplica
    }

    public enum NodeConnectionType
    {
        SSL,
        NON_SSL
    }
}
