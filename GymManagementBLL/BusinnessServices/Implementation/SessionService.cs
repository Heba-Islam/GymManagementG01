using AutoMapper;
using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models.SessionViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementSystemBLL.View_Models.SessionVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Implementation
{
    public class SessionService : ISessionService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region get all sessions 
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepository = _unitOfWork.SessionRepository;
            var sessions = sessionRepository.GetAllSessionsWithCategoryAndTrainer();

            return sessions.Select(session => new SessionViewModel
            {
                Id = session.Id,
                CategoryName = session.Category.CategoryName,
                Description = session.Description,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                TrainerName = $"{session.Trainer.Name}",
                Capacity = session.Capacity,
                AvailableSlots = session.Capacity - sessionRepository.GetCountOfBookedSlots(session.Id)
            }).ToList();
        }
        #endregion

        #region get all session's details 

        public SessionViewModel? GetSessionDetails(int sessionId)
        {
            var sessionRepository = _unitOfWork.SessionRepository;
            var session = sessionRepository.GetSessionWithTrainerAndCategory(sessionId);
            if (session is null) return null;

            var bookedSlots = sessionRepository.GetCountOfBookedSlots(sessionId);
            return new SessionViewModel
            {
                Id = session.Id,
                CategoryName = session.Category.CategoryName,
                Description = session.Description,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                TrainerName = $"{session.Trainer.Name}",
                Capacity = session.Capacity,
                AvailableSlots = session.Capacity - bookedSlots
            };
        }

        #endregion

        #region create a session
        public bool CreateASession(CreateSessionViewModel createSessionViewModel)
        {
            try
            {
                if (!DoesTrainerExists(createSessionViewModel.TrainerId) ||
              !DoesCategoryExists(createSessionViewModel.CategoryId) ||
              !IsValidTimeRange(createSessionViewModel.StartDate, createSessionViewModel.EndDate) ||
               createSessionViewModel.Capacity < 0 || createSessionViewModel.Capacity > 20)
                {
                    return false;
                }
                var session = _mapper.Map<CreateSessionViewModel, Session>(createSessionViewModel);
                _unitOfWork.GetRepository<Session>().Add(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region update a session
        public UpdateSessionViewModel? GetUpdatedSession(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (!IsSessionAvailable(session))
                return null;
            var updatedSessionViewModel = _mapper.Map<Session, UpdateSessionViewModel>(session);

            return updatedSessionViewModel;
        }

        public bool UpdateASession(int sessionId, UpdateSessionViewModel updateSessionViewModel)
        {
            try
            {
                var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
                if (!IsSessionAvailable(session) ||
                    !DoesTrainerExists(updateSessionViewModel.TrainerId) ||
                    !IsValidTimeRange(updateSessionViewModel.StartDate, updateSessionViewModel.EndDate))
                {
                    return false;
                }
                var updatedSession = _mapper.Map(updateSessionViewModel, session);
                updatedSession.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Session>().Update(updatedSession);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region delete a session
        public bool RemoveASession(int sessionId)
        {
            try
            {
                var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
                if (!IsSessionAvailableToDelete(session))
                    return false;
                _unitOfWork.GetRepository<Session>().Delete(session);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Helper methods
        private bool DoesTrainerExists(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        }
        private bool DoesCategoryExists(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }
        private bool IsValidTimeRange(DateTime startTime, DateTime endTime)
        {
            return startTime < endTime;
        }

        private bool IsSessionAvailable(Session session)
        {
            if (session == null)
                return false;
            if (session.StartTime < DateTime.Now || session.EndTime < DateTime.Now)
                return false;
            if (session.Capacity <= _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id))
                return false;
            return true;
        }
        private bool IsSessionAvailableToDelete(Session session)
        {
            if (session == null)
                return false;
            if (session.StartTime < DateTime.Now && session.EndTime > DateTime.Now)
                return false;
            if (session.StartTime > DateTime.Now)
                return false;
            if (session.Capacity <= _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id))
                return false;
            return true;
        }



        #endregion

    }
}
