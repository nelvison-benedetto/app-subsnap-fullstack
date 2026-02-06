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

        // 2️ Creo il domain entity
        var user = new User(
            id: null,    //new UserId(0),   //EF genererà l'ID se è identity sul db!
            email: new Email(command.Email),
            passwordHash: new PasswordHash(command.Password),
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow,
            lastLogin: null
        );

        await _userRepository.AddAsync(user);  // 2. Aggiungo al repository (senza SaveChanges!)
        await _unitOfWork.SaveChangesAsync();  // 3. Commit tramite UnitOfWork

        //ora hai 2 opzioni per ottenere l'id della nuova row aggiunta su db:
        //1.   ricarichi dal DB (DDD pulito e sicuro!), ma ti costa un'ulteriore query, ma ok
        var saved = await _userRepository.GetByEmailAsync(user.Email);
        //2.   un po piu violento sul DDD e puo essere overkill, ma eviti di fare nuova query
        //user.SetId(new UserId(entity.UserId)); // entity = EF tracked entity che ora ha l'ID

        return new UserResult(
            user!.Id!.Value.Value,
            user.Email.Value
        );

    }
}
