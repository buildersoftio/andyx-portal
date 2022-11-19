using System;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Producers
{
    public class ProducerDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string Topic { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ProducerInstanceType InstanceType { get; set; }

        public List<string> PublicIpRange { get; set; }
        public List<string> PrivateIpRange { get; set; }


        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }


        public ProducerDetailsViewModel()
        {
            InstanceType = ProducerInstanceType.Multiple;
        }
    }
    public enum ProducerInstanceType
    {
        Single,
        Multiple
    }
}
