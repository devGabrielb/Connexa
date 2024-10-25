using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Commons;
using Connexa.Domain.Events;

namespace Connexa.Domain.Entities
{
    public class User : Entity
    {
        private readonly List<Group> _familyGroups = [];
        private readonly List<Chore> _tasks = [];
        private readonly List<Comment> _comments = [];

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public IReadOnlyList<Group> FamilyGroups => [.. _familyGroups];
        public IReadOnlyList<Chore> Tasks => [.. _tasks];
        public IReadOnlyList<Comment> Comments => [.. _comments];


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