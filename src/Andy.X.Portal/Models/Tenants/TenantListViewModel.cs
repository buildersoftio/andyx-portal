using System.Collections.Generic;

namespace Andy.X.Portal.Models.Tenants
{
    public class TenantListViewModel
    {
        public List<string> Tenants { get; set; }

        public TenantListViewModel()
        {
            Tenants = new List<string>();
        }
    }
}
