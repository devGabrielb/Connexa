using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Commons;
using Harmoni.Domain.Events;

namespace Harmoni.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        private User() { }
        private User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
        public static User Create(string name, string email, string password)
        {
            var user = new User(

                name,
                email,
                password

            );

            user.RaiseDomainEvent(new CreatedUserEvent(user.Id));

            return user;
        }
    }
}