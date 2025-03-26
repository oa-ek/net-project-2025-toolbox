using AutoMapper;
using Core.DTOs;
using Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Power Tools
            CreateMap<PowerTool, PowerToolDto>().ReverseMap();
            CreateMap<PowerSupplyType, PowerSupplyTypeDto>().ReverseMap();
            CreateMap<ToolModel, ToolModelDto>().ReverseMap();
            CreateMap<ToolType, ToolTypeDto>().ReverseMap();

            // Hand Tools
            CreateMap<HandTool, HandToolDto>().ReverseMap();

            // Batteries
            CreateMap<Batary, BataryDto>().ReverseMap();
            CreateMap<BataryModel, BataryModelDto>().ReverseMap();

            // Brands & Conditions
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Condition, ConditionDto>().ReverseMap();

            // Users & Roles
            CreateMap<Worker, WorkerDto>().ReverseMap();
            CreateMap<Boss, BossDto>().ReverseMap();
            CreateMap<SystemAdmin, SystemAdminDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();

            // Locations
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}
