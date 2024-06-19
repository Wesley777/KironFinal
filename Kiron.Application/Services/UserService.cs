using FluentValidation;
using Kiron.Application.Interfaces;
using Kiron.Application.Models;
using Kiron.Application.Settings;
using Kiron.Application.Validation;
using Kiron.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kiron.Application.Services;

public class UserService : IUserService
{
    private const string GenericLoginFailureMessage = "Invalid login details!";
    private readonly IRepository _repository;
    private readonly IValidator<UserCredentials> _validator;
    private readonly AppSettings _appSettings;

    public UserService(IRepository repository,
                       AppSettings appSettings,
                       IValidator<UserCredentials> validator)
    {
        _repository = repository;
        _appSettings = appSettings;
        _validator = validator;
    }

    public async Task<(string messsage,bool doesUserExits)> CreateUser(string username, string password)
    {
        var validationResult = await _validator.ValidateAsync(
           new UserCredentials
           {
               Username = username,
               Password = password
           });

        if (!validationResult.IsValid)
        {
            var errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));

            return (errors, false);
        }

        var newUserCreated = await _repository.ExecuteStoredProcedureAsync<bool>("InsertUser", new { Username = username, Password = BCrypt.Net.BCrypt.HashPassword(password) });
     
        return newUserCreated ? ("Success, your account has been created!", true) : ("Username already exits!", false);
        
    }

    public async Task<(string messsage, bool doesUserExits)> Login(string username, string password)
    {
        var user = await _repository.ExecuteStoredProcedureAsync<User>("GetUserDetailsByUsername", new { Username = username });

        if (user is null)
        {
            return (GenericLoginFailureMessage, false);
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return (GenericLoginFailureMessage, false);
        }

        return (CreateToken(user), true);

    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_appSettings.Token));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(_appSettings.TokenDurationHours),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
