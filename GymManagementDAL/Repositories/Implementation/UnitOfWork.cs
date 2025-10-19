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
        private readonly GymDbContext _dbContext;
        public ISessionRepository SessionRepository { get; }

        public UnitOfWork()
        {

        }

        public UnitOfWork(GymDbContext dbContext , ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            SessionRepository = sessionRepository;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var TEntityType = typeof(TEntity);
            if (repositories.TryGetValue(TEntityType, out var repository))
                return (IGenericRepository<TEntity>)repository;

            var NewRepo = new GenericRepository<TEntity>(_dbContext);
            repositories[TEntityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
