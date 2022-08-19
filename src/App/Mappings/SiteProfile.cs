using App.Entities;
using App.Models;
using AutoMapper;

namespace App.Mappings
{
    public class SiteProfile : Profile
    {
        public SiteProfile()
        {
            CreateMap<AddSiteRequest, Site>();
        }
    }
}
