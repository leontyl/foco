using App.Entities;
using App.Models;
using AutoMapper;

namespace App.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CheckInRequest, Customer>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.PersonalId));
        }
    }
}
