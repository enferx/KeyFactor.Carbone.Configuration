using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration
{
    public class CarboneRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
    {
        public CarboneRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider: dbContextProvider)
        {
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            TEntity result = null;
            var saved = false;
            while (!saved)
            {
                try
                {
                    result = await base.UpdateAsync(entity, true, cancellationToken);
                    saved = true;
                }
                catch (AbpDbConcurrencyException ex)
                {
                    var uex = ex.InnerException as DbUpdateConcurrencyException;
                    foreach (var entry in uex.Entries)
                    {
                        if (entry.Entity is Entity)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = await entry.GetDatabaseValuesAsync();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                if (property.IsConcurrencyToken)
                                {
                                    proposedValues[property] = databaseValue;
                                }
                                else
                                {
                                    proposedValues[property] = proposedValue;
                                }

                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
            return result;
        }
    }
}
