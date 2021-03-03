using AutoMapper;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Barname, BarnameDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Contractor, ContractorDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Havaleh, HavalehDTO>().ReverseMap();
            CreateMap<NaftkeshDTO, Naftkesh>();
            CreateMap<Naftkesh, NaftkeshDTO>()
                .ForMember(dest => dest.ContractorName, opt => opt.MapFrom(src => src.Contractor.Name));
            CreateMap<NdepoType, NdepoTypeDTO>().ReverseMap();
            CreateMap<NdepoWorkShift, NdepoWorkShiftDTO>().ReverseMap();
            CreateMap<PermitType, PermitTypeDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<SendType, SendTypeDTO>().ReverseMap();
        }
    }
}