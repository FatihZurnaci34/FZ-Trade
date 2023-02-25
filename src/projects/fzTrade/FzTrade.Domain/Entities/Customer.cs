using Core.Persistence.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Domain.Entities
{
    public class Customer:User
    {
        public int UserId { get; set; }
        public int PhoneNumber { get; set; }

        public Customer()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public Customer(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash,
                bool status, AuthenticatorType authenticatorType, int userId, int phoneNumber) : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
            AuthenticatorType = authenticatorType;
            UserId = userId;
            PhoneNumber = phoneNumber;
        }
    }
}
