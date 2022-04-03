using System;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Tenants
{
    public class TenantDetailsViewModel
    {
        public string Name { get; set; }
        public TenantSettings Settings { get; set; }

        public TenantDetailsViewModel()
        {
            Settings = new TenantSettings();
        }
    }
    public class TenantSettings
    {
        public bool AllowProductCreation { get; set; }
        public string DigitalSignature { get; set; }
        public bool EnableEncryption { get; set; }
        public bool EnableGeoReplication { get; set; }
        public TenantLogging Logging { get; set; }

        public bool EnableAuthorization { get; set; }
        public List<TenantToken> Tokens { get; set; }


        // Split tenants by certificates will not be possible with version two
        public string CertificatePath { get; set; }

        public TenantSettings()
        {
            AllowProductCreation = true;
            EnableEncryption = false;

            EnableAuthorization = false;
            Tokens = new List<TenantToken>();


            EnableGeoReplication = false;
            Logging = TenantLogging.ERROR_ONLY;
        }
    }
    public enum TenantLogging
    {
        ALL,
        INFORMATION_ONLY,
        WARNING_ONLY,
        ERROR_ONLY,
    }

    public class TenantToken
    {
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
        public string IssuedFor { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
