using AutoMapper;
using Kiron.Application.Models;
using Kiron.Domain.Entities;

namespace Kiron.Application.Automapper;
public sealed class RegionProfile : Profile
{
    public RegionProfile()
    {
        CreateMap<Region, RegionDto>();
    }
}
