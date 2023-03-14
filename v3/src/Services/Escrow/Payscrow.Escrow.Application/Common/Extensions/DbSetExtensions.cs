using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Domain;
using System;
using System.Linq;

namespace Payscrow.Escrow.Application
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> ForTenant<T>(this DbSet<T> dataSet, Guid tenantId) where T : Entity
        {
            return dataSet.Where(x => x.TenantId == tenantId);
        }
    }
}