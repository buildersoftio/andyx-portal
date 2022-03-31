using System.Collections.Generic;

namespace Andy.X.Portal.Models.Consumers
{
    public class ConsumerListViewModel
    {
        public List<string> Consumers { get; set; }

        public ConsumerListViewModel()
        {
            Consumers = new List<string>();
        }
    }
}
