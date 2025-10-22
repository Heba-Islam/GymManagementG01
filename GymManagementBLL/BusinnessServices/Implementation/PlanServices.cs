using AutoMapper;
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

    namespace GymManagementBLL.BusinessServices.Implementation
    {
        internal class PlanService : IPlanService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            #region get all plans
            public IEnumerable<PlanViewModel> GetAllPlans()
            {
                var plans = _unitOfWork.GetRepository<Plan>().GetAll();

                if (plans is null || !plans.Any())
                    return [];

                return _mapper.Map<IEnumerable<PlanViewModel>>(plans);
            }

            #endregion

            #region get plan details
            public PlanViewModel? GetPlanDetails(int PlanId)
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

                if (plan is null) return null;

                return _mapper.Map<PlanViewModel>(plan);
            }
            #endregion

            #region get plan update details and update plan
            public PlanToUpdateViewModel? GetPlanDetailsToUpdate(int PlanId)
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

                if (plan is null || plan.IsActive == false || HasActiveMemberships(PlanId))
                    return null;

                return _mapper.Map<PlanToUpdateViewModel>(plan);
            }


            public bool UpdatePlan(int planId, PlanToUpdateViewModel planToUpdate)
            {
                var planRepository = _unitOfWork.GetRepository<Plan>();
                var plan = planRepository.GetById(planId);

                if (plan is null || planToUpdate is null)
                    return false;


                _mapper.Map(planToUpdate, plan);

                try
                {

                    planRepository.Update(plan);

                    return _unitOfWork.SaveChanges() > 0;
                }
                catch (Exception)
                {

                    return false;
                }

            }


            #endregion

            #region soft delete a plan
            public bool ToggleStatus(int planId)
            {
                var planRepo = _unitOfWork.GetRepository<Plan>();
                var plan = planRepo.GetById(planId);

                if (plan is null || HasActiveMemberships(planId))
                    return false;

                plan.IsActive = plan.IsActive == true ? false : true;

                plan.UpdatedAt = DateTime.Now;

                planRepo.Update(plan);

                return _unitOfWork.SaveChanges() > 0;
            } 
            #endregion

            #region Helper Methods

            private bool HasActiveMemberships(int planId)
            {
                var activeMemberships = _unitOfWork.GetRepository<Membership>()
                    .GetAll(X => X.PlanId == planId && X.Status == "Active");

                return activeMemberships.Any();
            }
            #endregion
        }
    }
}
