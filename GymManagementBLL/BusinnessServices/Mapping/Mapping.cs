using AutoMapper;
using GymManagementBLL.View_Models.SessionViewModel;
using GymManagementDAL.Entities;
using GymManagementSystemBLL.View_Models.SessionVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagementBLL.BusinnessServices.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore()
                );
            CreateMap<CreateSessionViewModel, Session>().ReverseMap();
        }
    }
}
