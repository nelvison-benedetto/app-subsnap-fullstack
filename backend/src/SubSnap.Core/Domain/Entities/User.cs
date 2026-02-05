using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.Entities;

//user domain semplice (no ICollection no EF no navigation props), w no list of SharedLink e Subscription 
public class User
{
    public UserId? Id { get; private set; }  //type other obj (readonly struct)(./ValueObjects/), COSI FAI LA VALIDAZIONE
        //nullable. verrà creato da DB
    public Email Email { get; private set; }   //type other obj  (readonly struct)(./ValueObjects/)
        //private set; perche in futuro voglio poterlo cambiare here only w e.g. method ChangeEmail()
    public PasswordHash PasswordHash { get; private set; }   //type other obj  (readonly struct)(./ValueObjects/)

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? LastLogin { get; private set; }  //private set; xk editabile solo here da method UpdateLastLogin()

    protected User() { }  //constructor!! x ORM only

    public User(   //constructor
        UserId? id,
        Email email,
        PasswordHash passwordHash,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime? lastLogin)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        LastLogin = lastLogin;
    }

    //internal void SetId(UserId id)  //IMPORTANTISSISMO! xk ti serve x obj entity-->domain obj
    //    //internal, solo .Infrastructure(stesso prj, assembly) puo usarlo!
    //{
    //    if (Id != null)
    //        throw new InvalidOperationException("Id already set");
    //    Id = id;
    //utilizza anche file .Core/AssemblyInfo.cs 
    //}  //SOLUZIONE ESTREMA PURISSIMA DDD, ma overkill!!!

    public void UpdateLastLogin(DateTime now)
    {
        LastLogin = now;
        UpdatedAt = now;
    }
}
