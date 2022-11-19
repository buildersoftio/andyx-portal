using System.Collections.Generic;

namespace Andy.X.Portal.Models.Producers
{
    public class ProducerListViewModel
    {
        public List<ProducerActivity> Producers { get; set; }
        public ProducerListViewModel()
        {
            Producers = new List<ProducerActivity>();
        }
    }

    public class ProducerActivity
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public int InstancesCount { get; set; }

        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string Topic { get; set; }
    }
}
