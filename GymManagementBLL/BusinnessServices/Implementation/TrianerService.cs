using GymManagementBLL.BusinnessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.MemberViewModels;
using GymManagementBLL.View_Models.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinnessServices.Implementation
{
    public class TrianerService : ITrianerService
    {

        private readonly IUnitOfWork _unitOfWork;

        public TrianerService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }


        #region Get All Trainers

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers is null || !trainers.Any()) return [];

            var ListOfTrainerViewModels = trainers.Select(m => new TrainerViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString(),
            });

            return ListOfTrainerViewModels;
        }


        #endregion

        #region create a trainer
        public bool CreateATrainer(CreateATrianerViewModel createATrianer)
        {
            try
            {
                if (doesEmailExist(createATrianer.Email) || doesPhoneExist(createATrianer.Phone))
                    return false;

                var trainer = new Trainer
                {
                    Name = createATrianer.Name,
                    Email = createATrianer.Email,
                    Phone = createATrianer.Phone,
                    BirthDay = createATrianer.DateOfBirth,
                    Address = new Address
                    {
                        City = createATrianer.City,
                        Street = createATrianer.Street,
                        BuildingNumber = createATrianer.BuildingNumber,
                    },
                    Speciality = createATrianer.Specialization,
                    CreatedAt = DateTime.Now,

                };


                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #endregion

        #region get trainer details
        public TrainerViewModel? GetMemberDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;

            var trainerViewModel = new TrainerViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString(),
                BirthDay = trainer.BirthDay.ToShortDateString(),
                Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}",
                Speciality = trainer.Speciality,

            };

            
            return trainerViewModel;
        }
        #endregion

        #region Get Trainer Details To Update
        public TrainerToUpdateViewModel? GetTrainerDetailsToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer is null) return null;

            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                Speciality = trainer.Speciality
            };
        }

        #endregion

        #region Update trainer
        public bool UpdateTrainer(int tainerId, TrainerToUpdateViewModel trainerToUpdate)
        {
            try
            {
                if (doesEmailExist(trainerToUpdate.Email) || doesPhoneExist(trainerToUpdate.Phone))
                       return false;

                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(tainerId);
                if (trainer is null) return false;

                trainer.Phone = trainerToUpdate.Phone;
                trainer.Email = trainerToUpdate.Email;
                trainer.Address.BuildingNumber = trainerToUpdate.BuildingNumber;
                trainer.Address.Street = trainerToUpdate.Street;
                trainer.Address.City = trainerToUpdate.City;
                trainer.Speciality = trainerToUpdate.Speciality;
                trainer.UpdatedAt = DateTime.Now;

                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {

                return false;
            }
                
        }

        #endregion

        #region remove a trainer

        public bool DeleteTrainer(int trainerId)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
                if (trainer is null) return false;

                var HasActiveSession = _unitOfWork.GetRepository<Session>()
                    .GetAll(x => x.TrainerId == trainerId && x.EndTime < DateTime.Now).Any();

                if (HasActiveSession) return false;

                trainer.Sessions.Clear();
                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion

        #region helper methods

        private bool doesEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        }

        private bool doesPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
        }


        #endregion
    }
}
