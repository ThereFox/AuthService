using Application.InputObjects;
using Application.Interfaces.Getter;
using Application.Interfaces.TokenOnUser;
using Application.UseCases;
using AuntificationService.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebAPI.InputObjects;

namespace WebAPI.Controllers;

[ApiController]
[Route("public")]
public class RegistrationController : Controller
{
    private readonly AuthentificateUseCase _authentificateUseCase;
    private readonly RegistrateUseCase _registrateUseCase;
    private readonly LogOutUseCase _logOutUseCase;
    private readonly ITokenGetter _getter;
    private readonly ITokenSetter _setter;

    public RegistrationController(
        AuthentificateUseCase authentificateUseCase,
        RegistrateUseCase registrateUseCase,
        LogOutUseCase logOutUseCase,
        ITokenGetter getter,
        ITokenSetter setter
        )
    {
        _authentificateUseCase = authentificateUseCase;
        _registrateUseCase = registrateUseCase;
        _logOutUseCase = logOutUseCase;
        _getter = getter;
        _setter = setter;
    }

    [HttpPost]
    [Route("reg")]
    public async Task<IActionResult> Register([FromBody] RegistrationInfoInputObject request)
    {
        var inputObject = new RegistrateInputObject(request.Login, request.Password, "test@test.com");
        
        var res = await _registrateUseCase.Registrate(inputObject);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInInfoInputObject request)
    {
        var validateCredentials = AuthCredentials.Create(request.Login, request.Password);

        if (validateCredentials.IsFailure)
        {
            return BadRequest();
        }
        
        var authResult = await _authentificateUseCase.Authintificate(validateCredentials.Value);

        if (authResult.IsFailure)
        {
            return BadRequest(authResult.Error);
        }
        return Ok();
    }
    
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> LogOut()
    {
        var logoutResult = _logOutUseCase.LogOut();

        if (logoutResult.IsFailure)
        {
            return BadRequest(logoutResult.Error);
        }
        
        return Ok();
    }
}