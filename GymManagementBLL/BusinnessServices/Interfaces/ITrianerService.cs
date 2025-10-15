using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.MemberViewModels;
using GymManagementBLL.View_Models.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Interfaces
{
    public interface ITrianerService
    {
        public IEnumerable<TrainerViewModel> GetAllTrainers();

        public bool CreateATrainer(CreateATrianerViewModel createATrianer);
        public TrainerViewModel? GetMemberDetails(int memberId);

        public TrainerToUpdateViewModel? GetTrainerDetailsToUpdate(int trainerId);

        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdate);

        public bool DeleteTrainer(int trainerId);

    }
}
