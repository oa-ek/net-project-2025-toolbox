using AutoMapper;
using Core.DTOs;

namespace Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Power Tools
            CreateMap<PowerTool, PowerToolDto>()
                .ForMember(dest => dest.ConditionName, opt => opt.MapFrom(src => src.Condition.Name))
                .ForMember(dest => dest.ToolTypeName, opt => opt.MapFrom(src => src.ToolType.Name))
                .ForMember(dest => dest.LastWorkerId, opt => opt.MapFrom(src => src.LastWorkerId))
                .ForMember(dest => dest.LastLocationId, opt => opt.MapFrom(src => src.LastLocationId));

            CreateMap<PowerToolDto, PowerTool>()
                .ForMember(dest => dest.Condition, opt => opt.Ignore()) // Уникаємо створення нового Condition
                .ForMember(dest => dest.ToolType, opt => opt.Ignore()) // Уникаємо створення нового ToolType
                .ForMember(dest => dest.LastWorker, opt => opt.Ignore()) // Уникаємо створення нового Worker
                .ForMember(dest => dest.LastWorkerId, opt => opt.MapFrom(src => src.LastWorkerId));



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
            CreateMap<Worker, WorkerDto>()
    .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name))
    .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
    .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.PositionId))
    .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.LocationId))
    .ForMember(dest => dest.BossId, opt => opt.MapFrom(src => src.BossId));

            CreateMap<WorkerDto, Worker>()
                .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Boss, opt => opt.Ignore())
                .ForMember(dest => dest.Bryhadyr, opt => opt.Ignore());

            CreateMap<Boss, BossDto>().ReverseMap();
            CreateMap<SystemAdmin, SystemAdminDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();

            // Locations
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}
