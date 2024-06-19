using AutoMapper;
using Kiron.Application.Models;
using Kiron.Domain.Entities;

namespace Kiron.Application.Automapper;
public sealed class HolidayProfile : Profile
{
    public HolidayProfile()
    {
        CreateMap<Holiday, HolidayDto>();
    }
}
