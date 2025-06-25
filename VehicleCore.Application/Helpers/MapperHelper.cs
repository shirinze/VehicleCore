using AutoMapper;
using System.Runtime.CompilerServices;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainModel.Models;

namespace VehicleCore.Application.Helpers;

public static class MapperHelper
{
    public static CarViewModel ToViewModel(this Car entity)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Car, CarViewModel>()
            .ForMember(dest => dest.GearBox, opt => opt.MapFrom(src => src.GearBox.ToString()));
        });

        var mapper = new Mapper(config);

        return mapper.Map<CarViewModel>(entity);

    }
    public static List<CarViewModel> ToViewModel(this List<Car> entities)
    {
        return entities.Select(x => new CarViewModel
        {
            Id = x.Id,
            Title = x.Title,
            TrackingCode = x.TrackingCode,
            GearBox = x.GearBox.ToString(),
        }).ToList();
    }
    public static MotorcycleViewModel ToViewModel(this Motorcycle entity)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Motorcycle, MotorcycleViewModel>()
            .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel.ToString()));
        });

        var mapper = new Mapper(config);

        return mapper.Map<MotorcycleViewModel>(entity);

    }
    public static List<MotorcycleViewModel> ToViewModel(this List<Motorcycle> entities)
    {
        return entities.Select(x => new MotorcycleViewModel
        {
            Id = x.Id,
            Title = x.Title,
            TrackingCode = x.TrackingCode,
            Fuel = x.Fuel.ToString(),
        }).ToList();
    }



}
