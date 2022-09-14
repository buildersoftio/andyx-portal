using Andy.X.Portal.Models.Products;
using System;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Tenants
{
    public class TenantDetailsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }


        public TenantSettings Settings { get; set; }
        public List<Token> Tokens { get; set; }

        public List<string> Products { get; set; }
        public List<TenantRetention> TenantRetentions { get; set; }

        public TenantDetailsViewModel()
        {
            Settings = new TenantSettings();
            Tokens = new List<Token>();
            Products = new List<string>();
            TenantRetentions = new List<TenantRetention>();
        }
    }
    public class TenantSettings
    {
        public bool IsProductAutomaticCreationAllowed { get; set; }
        public bool IsEncryptionEnabled { get; set; }
        public bool IsAuthorizationEnabled { get; set; }

        public TenantSettings()
        {
            IsProductAutomaticCreationAllowed = false;
            IsEncryptionEnabled = false;
            IsAuthorizationEnabled = false;
        }
    }

    public class Token
    {
        public Guid Key { get; set; }
        public string Secret { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public bool IsActive { get; set; }

        public Token()
        {
            Key = Guid.Empty;
            Secret = "*************************";
        }
    }

    public enum TenantTokenRole
    {
        Produce,
        Consume
    }

    public class TenantRetention
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public RetentionType Type { get; set; }
        public long TimeToLiveInMinutes { get; set; }
    }

    public enum RetentionType
    {
        SOFT_TTL,
        HARD_TTL
    }
}
