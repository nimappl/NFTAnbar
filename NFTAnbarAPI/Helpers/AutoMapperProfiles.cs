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
            CreateMap<Naftkesh, NaftkeshDTO>().ReverseMap();
            CreateMap<Ndepo, NdepoDTO>().ReverseMap();
            CreateMap<NdepoType, NdepoTypeDTO>().ReverseMap();
            CreateMap<NdepoWorkShift, NdepoWorkShiftDTO>().ReverseMap();
            CreateMap<Permit, PermitDTO>().ReverseMap();
            CreateMap<PermitType, PermitTypeDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<SendType, SendTypeDTO>().ReverseMap();
            CreateMap<KhzemanatNamehType, KhzemanatNamehTypeDTO>().ReverseMap();
        }
    }
}