using App.Entities;
using App.Models;
using AutoMapper;

namespace App.Mappings
{
    public class QueueProfile : Profile
    {
        public QueueProfile()
        {
            CreateMap<Queue, GetNextActionResponse>()
                .ForMember(dst => dst.CaseNumber, opt => opt.MapFrom(src => src.TicketNumber))
                .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dst => dst.PersonalId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dst => dst.DateOfBirth, opt => opt.MapFrom(src => src.Customer.DateOfBirth))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                ;
        }
    }
}
