using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext dbContext;
        public TrainerRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Trainer> GetAll() => dbContext.Trainers.ToList();

        public Entities.Trainer? GetById(int id) => dbContext.Trainers.Find(id);

        public int Add(Trainer trainer)
        {
            dbContext.Trainers.Add(trainer);
            return dbContext.SaveChanges();
        }
        public int Update(Trainer trainer)
        {
            dbContext.Trainers.Update(trainer);
            return dbContext.SaveChanges();
        }
        public int Delete(Trainer trainer)
        {
            dbContext.Trainers.Remove(trainer);
            return dbContext.SaveChanges();
        }

    }
}
