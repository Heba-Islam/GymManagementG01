using GymManagementBLL.View_Models.SessionViewModel;
using GymManagementSystemBLL.View_Models.SessionVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Interfaces
{
    public interface ISessionService
    {
        public IEnumerable<SessionViewModel> GetAllSessions();

        public SessionViewModel? GetSessionDetails(int sessionId);
        public bool CreateASession(CreateSessionViewModel createSessionViewModel);
        public UpdateSessionViewModel? GetUpdatedSession(int sessionId);
        public bool UpdateASession(int sessionId, UpdateSessionViewModel updateSessionViewModel);
        public bool RemoveASession(int sessionId);

    }
}
