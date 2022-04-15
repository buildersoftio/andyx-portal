using Andy.X.Portal.Models.Components;
using Andy.X.Portal.Models.Consumers;
using Andy.X.Portal.Models.Producers;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Lineage
{
    public class StreamLineageViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }

        public List<StreamLineage> StreamLineages { get; set; }

        public StreamLineageViewModel()
        {
            StreamLineages = new List<StreamLineage>();
        }
    }

    public class StreamLineage
    {
        public List<ProducerDetailsViewModel> Producers { get; set; }
        public string Topic { get; set; }
        public string TopicPhysicalPath { get; set; }
        public List<ConsumerDetailsViewModel> Consumers { get; set; }

        public StreamLineage()
        {
            Producers = new List<ProducerDetailsViewModel>();
            Consumers = new List<ConsumerDetailsViewModel>();
        }
    }
}
