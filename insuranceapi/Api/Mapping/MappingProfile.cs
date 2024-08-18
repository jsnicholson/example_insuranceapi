using Api.Models;
using AutoMapper;
using Data.Entities;

namespace Api.Mapping {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<CreateClaimRequest, Claim>()
                .ForMember(dest => dest.UniqueClaimReference, opt => opt.MapFrom(src => src.UniqueClaimReference))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.ClaimDate, opt => opt.MapFrom(src => src.ClaimDate))
                .ForMember(dest => dest.LossDate, opt => opt.MapFrom(src => src.LossDate))
                .ForMember(dest => dest.AssuredName, opt => opt.MapFrom(src => src.AssuredName))
                .ForMember(dest => dest.IncurredLoss, opt => opt.MapFrom(src => src.IncurredLoss))
                .ForMember(dest => dest.Closed, opt => opt.MapFrom(src => src.Closed));
        }
    }
}
