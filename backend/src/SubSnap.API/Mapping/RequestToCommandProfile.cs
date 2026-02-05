using AutoMapper;
using SubSnap.Core.DTOs.Application.Commands.Users;
using SubSnap.Core.DTOs.External.Requests.Users;

namespace SubSnap.API.Mapping;

public sealed class RequestToCommandProfile : Profile
{
    public RequestToCommandProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>();
    }
}
