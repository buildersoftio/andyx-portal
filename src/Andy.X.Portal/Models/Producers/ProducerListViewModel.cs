using System.Collections.Generic;

namespace Andy.X.Portal.Models.Producers
{
    public class ProducerListViewModel
    {
        public List<string> Producers { get; set; }
        public ProducerListViewModel()
        {
            Producers = new List<string>();
        }
    }
}
