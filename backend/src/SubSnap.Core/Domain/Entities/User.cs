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
    public UserId Id { get; }  //type other obj (readonly struct)(./ValueObjects/)
    public Email Email { get; private set; }   //type other obj  (readonly struct)(./ValueObjects/)
    public PasswordHash PasswordHash { get; private set; }   //type other obj  (readonly struct)(./ValueObjects/)

    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? LastLogin { get; private set; }

    protected User() { }  //constructor!!

    public User(   //constructor
        UserId id,
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

    public void UpdateLastLogin(DateTime now)
    {
        LastLogin = now;
        UpdatedAt = now;
    }
}
