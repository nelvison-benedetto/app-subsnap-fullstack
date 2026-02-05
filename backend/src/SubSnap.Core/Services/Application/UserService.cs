using SubSnap.Core.Contracts.Repositories;
using SubSnap.Core.Contracts.Services;
using SubSnap.Core.Contracts.UnitOfWork;
using SubSnap.Core.Domain.Entities;
using SubSnap.Core.Domain.Exceptions;
using SubSnap.Core.Domain.ValueObjects;
using SubSnap.Core.DTOs.Application.Commands.Users;
using SubSnap.Core.DTOs.Application.Results.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Services.Application;

//no EF, no DBO 
//transazione controllata, orchestration pulita
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResult> RegisterAsync(RegisterUserCommand command)
    {
        // 1 Controllo se esiste già l'email
        var existing = await _userRepository.GetByEmailAsync(new Email(command.Email));
        if (existing != null)
            throw new EmailAlreadyRegisteredException(command.Email);

        // 2️ Creo il domain object
        var user = new User(
            new UserId(0),   //EF genererà l'ID se è identity sul db!
            new Email(command.Email),
            new PasswordHash(command.Password),
            DateTime.UtcNow,
            DateTime.UtcNow,
            null
        );

        // 3️ Aggiungo al repository
        await _userRepository.AddAsync(user);

        // 4️ Commit tramite UnitOfWork
        await _unitOfWork.SaveChangesAsync();

        // 5️ Mapping a Result
        return new UserResult( 
            user.Id.Value,
            user.Email.Value
        );  //uso il constct del record UserResult
    }
}
