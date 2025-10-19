using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.implementation;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Implementation
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;
        public SessionRepository(GymDbContext DBContext): base(DBContext)
        {
            _dbContext = DBContext;
        }
        public IEnumerable<Session> GetAllSessionsWithCategoryAndTrainer()
        {
            return _dbContext.Sessions
                .Include(s => s.Category)
                .Include(s => s.Trainer)
                .ToList();
        }



        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions
                .Count(b => b.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            var session = _dbContext.Sessions
                .Include(x => x.Trainer)
                .Include(x => x.Category)
                .FirstOrDefault(s => s.Id == sessionId);

            if (session is null) return null;
            return session;

        }
    }
}
