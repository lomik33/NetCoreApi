
using EmpleadoDefinicion.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_NetCore.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder){

        }

        protected override void OnModelCreating(ModelBuilder builder){
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new UnidadMedidaConfiguration());
            builder.ApplyConfiguration(new ComidaConfiguration());
            builder.ApplyConfiguration(new EmpleadoConfiguration());
            builder.ApplyConfiguration(new InformacionContratoConfiguration());
            base.OnModelCreating(builder);
        }
        public override int SaveChanges(){
            foreach (var entry in ChangeTracker.Entries()){
                                    DateTime ahora = DateTime.UtcNow;
                if (entry.Entity is IDateObserver){
                    switch (entry.State){
                        case EntityState.Added:
                        ((IDateObserver)entry.Entity).CreatedAt = ahora;
                        break;
                        case EntityState.Modified:
                                                ((IDateObserver)entry.Entity).ModifiedAt = ahora;
                        break;
                    }
                }
                if (entry.Entity is ISoftDelete){
                    if (entry.State == EntityState.Deleted){
                                                ((ISoftDelete)entry.Entity).IsDeleted = true;
                                                ((ISoftDelete)entry.Entity).DateDeleted= ahora;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
