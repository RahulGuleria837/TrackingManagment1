using AutoMapper;

namespace TrackingManagment.Models.DTO.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<RealState , RealStateDTO>().ReverseMap();
        }

    }
}
