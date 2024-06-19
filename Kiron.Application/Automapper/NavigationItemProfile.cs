using AutoMapper;
using Kiron.Application.Models;
using Kiron.Domain.Entities;

namespace Kiron.Application.Automapper;

public sealed class NavigationItemProfile : Profile
{
    public NavigationItemProfile()
    {
        CreateMap<NavigationItem, NavigationItemDto>();
    }
}
