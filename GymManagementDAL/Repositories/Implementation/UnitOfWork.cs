using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> repositories = new();
        private readonly GymDbContext dbContext;

        public UnitOfWork()
        {

        }

        public UnitOfWork(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var TEntityType = typeof(TEntity);
            if (repositories.TryGetValue(TEntityType, out var repository))
                return (IGenericRepository<TEntity>)repository;

            var NewRepo = new GenericRepository<TEntity>(dbContext);
            repositories[TEntityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}
