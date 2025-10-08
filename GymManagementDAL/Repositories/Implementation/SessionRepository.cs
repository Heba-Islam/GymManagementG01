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
    public class SessionRepository
    {
        public class SessionRepository : ISessionRepository
        {
            private readonly GymDbContext dbContext;
            public SessionRepository(GymDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public IEnumerable<Session> GetAll() => dbContext.Sessions.ToList();
            public Session? GetById(int id) => dbContext.Sessions.Find(id);

            public int Add(Session session)
            {
                dbContext.Sessions.Add(session);
                return dbContext.SaveChanges();
            }
            public int Update(Session session)
            {
                dbContext.Sessions.Update(session);
                return dbContext.SaveChanges();
            }
            public int Delete(int id)
            {
                var session = dbContext.Sessions.Find(id);
                if (session is null) return 0;
                dbContext.Sessions.Remove(session);
                return dbContext.SaveChanges();
            }
        }
    }
}
