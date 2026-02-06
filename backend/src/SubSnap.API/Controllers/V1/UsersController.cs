using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SubSnap.API.Contracts.Responses;
using SubSnap.API.Validators;
using SubSnap.Core.Contracts.Services;
using SubSnap.Core.Domain.ValueObjects;
using SubSnap.Core.DTOs.Application.Commands.Users;
using SubSnap.Core.DTOs.External.Requests.Users;
using SubSnap.Core.DTOs.External.Responses.Users;
using SubSnap.Core.Services.Application;

namespace SubSnap.API.Controllers.V1;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IValidator<RegisterUserCommand> _validator;

    public UsersController( IMapper mapper, IUserService userService, IValidator<RegisterUserCommand> validator)
    {
        _mapper = mapper;
        _userService = userService;
        _validator = validator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResult<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResult<UserResponse>>> Register(
        RegisterUserRequest request)
    {
        // Request -> Command
        var command = _mapper.Map<RegisterUserCommand>(request);

        // Validazione centralizzata
        await ValidatorHelper.ValidateCommandAsync(_validator, command);

        // Application Layer
        var result = await _userService.RegisterAsync(command);

        // Result -> Response
        var response = _mapper.Map<UserResponse>(result);

        return Ok(ApiResult<UserResponse>.Ok(response));
    }
    
}
