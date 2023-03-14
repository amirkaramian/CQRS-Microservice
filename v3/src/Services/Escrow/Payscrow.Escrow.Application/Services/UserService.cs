using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Services
{
    public class UserService : ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;
        private readonly ICacheManager _cache;

        public UserService(IEscrowDbContext context, ICacheManager cache)
        {
            _context = context;
            _cache = cache;
        }

        public const string USER_BY_GUID = "payscrow.escrow.user-{0}-{1}";

        public async Task<User> GetUserAsync(Guid userGuid, Guid tenantId)
        {
            string key = string.Format(USER_BY_GUID, userGuid, tenantId);

            return await _cache.GetAsync(key, 24 * 60, async () =>
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserGuid == userGuid && x.TenantId == tenantId);

                //if (user == null)
                //{
                //    user = new User
                //    {
                //        UserGuid = userGuid,
                //        Email = _identityService.Email
                //    };

                //    _context.Users.Add(user);
                //    await _context.SaveChangesAsync();
                //}

                return user;
            });
        }

        public void RemoveUserFromCache(Guid userGuid, Guid tenantId)
        {
            string key = string.Format(USER_BY_GUID, userGuid, tenantId);

            _cache.Remove(key);
        }
    }
}