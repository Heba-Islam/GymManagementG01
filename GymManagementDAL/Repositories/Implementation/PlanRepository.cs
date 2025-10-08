using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Implementation
{
    public class PlanRepository(GymDbContext dbContext) : IPlanRepository
    {
        private readonly GymDbContext dbContext = dbContext;

        public IEnumerable<Plan> GetAll() => dbContext.Plans.ToList();

        public Plan? GetById(int id) => dbContext.Plans.Find(id);

        public int Add(Plan plan)
        {
            dbContext.Plans.Add(plan);
            return dbContext.SaveChanges();
        }
        public int Update(Plan plan)
        {

            dbContext.Plans.Update(plan);
            return dbContext.SaveChanges();
        }
        public int Delete(Plan plan)
        {

            dbContext.Plans.Remove(plan);
            return dbContext.SaveChanges();
        }
    }

}
