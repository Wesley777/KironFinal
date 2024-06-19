namespace Kiron.Application.Services;

public interface IUserService
{
    Task<(string messsage, bool doesUserExits)> CreateUser(string username, string password);
    Task<(string messsage, bool doesUserExits)> Login(string username, string password);
}