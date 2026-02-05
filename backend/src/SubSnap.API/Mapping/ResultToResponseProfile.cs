using AutoMapper;
using SubSnap.Core.DTOs.Application.Results.Users;
using SubSnap.Core.DTOs.External.Responses.Users;

namespace SubSnap.API.Mapping;

public sealed class ResultToResponseProfile : Profile
{
    public ResultToResponseProfile()
    {
        CreateMap<UserResult, UserResponse>();
    }
}
