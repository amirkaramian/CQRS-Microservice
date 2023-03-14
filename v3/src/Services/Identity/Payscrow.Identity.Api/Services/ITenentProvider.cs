using System;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Services
{
    public interface ITenentProvider
    {
        Task<Guid> GetTenantIdAsync();
        Task<Guid> GetTenantIdAsync(string hostName);
        Task<TenantPreference> GetTenantPreferenceAsync();
    }


    public class TenantPreference
    {
        public string Colour { get; set; }
        public string LogoUrl { get; set; }
    }
}
