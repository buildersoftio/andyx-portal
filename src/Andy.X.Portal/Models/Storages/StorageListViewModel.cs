using System.Collections.Generic;

namespace Andy.X.Portal.Models.Storages
{
    public class StorageListViewModel
    {
        public List<string> StorageNames { get; set; }
        public StorageListViewModel()
        {
            StorageNames = new List<string>();
        }
    }
}
