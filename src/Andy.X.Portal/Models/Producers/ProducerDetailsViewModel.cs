using System;

namespace Andy.X.Portal.Models.Producers
{
    public class ProducerDetailsViewModel
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
        public string Component { get; set; }
        public string Topic { get; set; }

        public bool IsLocal { get; set; }

        public Guid Id { get; set; }
        public string ProducerName { get; set; }

        public DateTime ConnectedDate { get; set; }
        public long CountMessagesProducedSinceConnected { get; set; }


        public ProducerDetailsViewModel()
        {
            IsLocal = true;
        }
    }
}
