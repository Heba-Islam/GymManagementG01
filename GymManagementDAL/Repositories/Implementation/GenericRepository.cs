using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext dbContext;

        public GenericRepository()
        {

        }
        public GenericRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            return dbContext.SaveChanges();

        }

        public int Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return dbContext.SaveChanges();

        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition == null)
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
            {
                return dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
            }
        }


        public TEntity? GetById(int id)
        => dbContext.Set<TEntity>().Find(id);

        public int Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return dbContext.SaveChanges();
        }
    }
}
