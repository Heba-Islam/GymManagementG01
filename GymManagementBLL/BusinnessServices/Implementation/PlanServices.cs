using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Implementation
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Get all plans
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans is null || !plans.Any()) return [];

            var allPlans = plans.Select(x => new PlanViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DurationDays = x.DurationDays,
                IsActive = x.IsActive,
                Price = x.Price
            });
            return allPlans;
        }
        #endregion

        #region Get plan details
        public PlanViewModel? GetPlanDetails(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null) return null;

            return new PlanViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive,
                Price = plan.Price
            };
        }
        #endregion

        #region Plan to update details
        public PlanToUpdateViewModel? GetPlanDetailsToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null || plan.IsActive == false || HasActiveMemberships(planId)) return null;

            return new PlanToUpdateViewModel()
            {
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };
        }
        #endregion

        #region update plan 
        public bool UpdatePlan(int planId, PlanToUpdateViewModel planToUpdate)
        {
            try
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
                if (plan is null || HasActiveMemberships(planId)) return false;


                plan.Description = planToUpdate.Description;
                plan.Price = planToUpdate.Price;
                plan.DurationDays = planToUpdate.DurationDays;
                plan.UpdatedAt = DateTime.Now;

                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region remove  a plan --soft delete
        //delte plan
        public bool ToggleStatus(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null || HasActiveMemberships(planId)) return false;

            plan.IsActive = plan.IsActive == true ? false : true;
            plan.UpdatedAt = DateTime.Now;

            try
            {
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception )
            {
                return false;
            }
        }

        #endregion

        #region Helper methods

        private bool HasActiveMemberships(int planId)
        {
            var ActiveMemberShip = _unitOfWork.GetRepository<Membership>().GetAll(x => x.PlanId == planId && x.Status == "Active").Any();
            return ActiveMemberShip;
        }

        #endregion
    }
}
