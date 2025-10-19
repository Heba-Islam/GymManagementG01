using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository: IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithCategoryAndTrainer();

        Session? GetSessionWithTrainerAndCategory(int sessionId);
        int GetCountOfBookedSlots (int sessionId);
    }
}
