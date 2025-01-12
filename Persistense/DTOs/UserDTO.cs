using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace Persistense.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public int RoleId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    
    public List<TokensPairDTO> Tokens { get; set; }
}

public static class UserConverters
{
    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Login = user.Credentials.Login,
            Password = user.Credentials.PasswordHash,
            RoleId = user.Role.RoleId
        };
    }
    public static Result<User> ToDomain(this UserDTO user)
    {
        var validateRole = UserRole.Create(user.RoleId);
        var validateCredentials = AuthCredentials.Create(user.Login, user.Password);

        if (validateRole.IsFailure || validateCredentials.IsFailure)
        {
            return Result.Failure<User>("invalid input");
        }
        
        var validateUser = User.Create(user.Id, validateRole.Value, validateCredentials.Value, null);

        return validateUser;
    }
}