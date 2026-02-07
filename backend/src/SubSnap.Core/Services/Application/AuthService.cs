using SubSnap.Core.Abstractions.Identity;
using SubSnap.Core.Contracts.Repositories;
using SubSnap.Core.Contracts.UnitOfWork;
using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Services.Application;

public class AuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _uow;

    public AuthService(
        IUserRepository userRepo,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _uow = uow;
    }

    public async Task<(string accessToken, string refreshToken)> LoginAsync(Email email, string plainPassword)
    {
        var user = await _userRepo.GetByEmailAsync(email)
            ?? throw new Exception("Invalid credentials");

        if (!_passwordHasher.Verify(plainPassword, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.AddRefreshToken(refreshToken, DateTime.UtcNow.AddDays(30));
        await _uow.SaveChangesAsync();
        return (accessToken, refreshToken);
    }

}
