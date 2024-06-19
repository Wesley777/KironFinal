using Kiron.Application.Models;
using Kiron.Domain.Entities;

namespace Kiron.Application.Services;
public interface INavigationService
{
    Task<IEnumerable<NavigationItemDto>> GetNavigationItems();
}