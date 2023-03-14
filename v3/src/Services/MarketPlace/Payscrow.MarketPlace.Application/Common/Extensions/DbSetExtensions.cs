using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Domain;
using System;
using System.Linq;

namespace Payscrow.MarketPlace.Application
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> ForTenant<T>(this DbSet<T> dataSet, Guid tenantId) where T : Entity
        {
            return dataSet.Where(x => x.TenantId == tenantId);
        }
    }
}
