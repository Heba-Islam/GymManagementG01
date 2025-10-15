using GymManagementBLL.View_Models.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Interfaces
{
    public interface IPlanService
    {
        public IEnumerable<PlanViewModel> GetAllPlans();
        public PlanViewModel? GetPlanDetails(int planId);
        public PlanToUpdateViewModel? GetPlanDetailsToUpdate(int planId);

        public bool UpdatePlan(int planId, PlanToUpdateViewModel planToUpdate);

        //soft delete a plan 
        public bool ToggleStatus(int planId);

    }
}
