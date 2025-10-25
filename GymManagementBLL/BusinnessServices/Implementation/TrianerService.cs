using AutoMapper;
using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.BusinnessServices.Implementation
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region create a trainer
        public bool CreateATrainer(CreateATrainerViewModel createTrainer)
        {
            if (createTrainer is null || DoesEmailExist(createTrainer.Email)
                || DoesPhoneExist(createTrainer.Phone))
                return false;


            var trainer = _mapper.Map<CreateATrainerViewModel, Trainer>(createTrainer);


            try
            {
                _unitOfWork.GetRepository<Trainer>().Add(trainer);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }


        }
        #endregion


        #region get all trainers
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            if (trainers is null || !trainers.Any())
                return [];

            return _mapper.Map<IEnumerable<TrainerViewModel>>(trainers);

        }
        #endregion


        #region delete a trainer
        public bool DeleteTrainer(int trainerId)
        {
            var trainerRepository = _unitOfWork.GetRepository<Trainer>();

            var trainer = trainerRepository.GetById(trainerId);
            if (trainer is null || HasFutureSessions(trainerId))
                return false;
            try
            {

                trainerRepository.Delete(trainer);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion


        #region get trainer details
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer is null) return null;

            return _mapper.Map<TrainerViewModel>(trainer);
        }
        #endregion

        #region get trainer details to update and update a trainer
        public TrainerToUpdateViewModel? GetTrainerDetailsToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;
            return _mapper.Map<TrainerToUpdateViewModel>(trainer);
        }

        public bool UpdateTrainer(int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            var trainerRepository = _unitOfWork.GetRepository<Trainer>();

            var EmailExistForAnotherOldTrainer = trainerRepository
                 .GetAll(X => X.Email == trainerToUpdate.Email && X.Id != id)
                 .Any();

            var phoneExistForAnotherOldTrainer = trainerRepository
                .GetAll(X => X.Phone == trainerToUpdate.Phone && X.Id != id)
                .Any();

            if (EmailExistForAnotherOldTrainer || phoneExistForAnotherOldTrainer || trainerToUpdate is null)
                return false;


            var trainer = trainerRepository.GetById(id);

            if (trainer is null) return false;

            _mapper.Map(trainerToUpdate, trainer);

            try
            {
                trainerRepository.Update(trainer);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }


        } 
        #endregion


        #region HelperMethod

        private bool DoesEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
        }

        private bool DoesPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Phone == phone).Any();
        }


        private bool HasFutureSessions(int trainerId)
        {
            return _unitOfWork.GetRepository<Session>()
                .GetAll(S => S.TrainerId == trainerId && S.StartTime > DateTime.Now)
                .Any();
        }
        #endregion

    }
}