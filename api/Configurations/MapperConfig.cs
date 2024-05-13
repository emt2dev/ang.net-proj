using AuthReadyAPI.DataLayer.DTOs.Employees;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.TaxCalculator;
using AuthReadyAPI.DataLayer.Models.Employees;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.TaxCalculator;
using AutoMapper;

namespace AuthReadyAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<APIUserClass, APIUserDTO>().ReverseMap();
            CreateMap<EmployeeClass, EmployeeDTO>().ReverseMap();
            CreateMap<TaxPayoutsClass, TaxPayoutsDTO>().ReverseMap();
        }
    }
}
