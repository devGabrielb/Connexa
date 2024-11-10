using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connexa.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        private RefreshToken(string token, Guid userId, DateTime expires, DateTime created)
        {
            Token = token;
            UserId = userId;
            Expires = expires;
            Created = created;
        }

        public static RefreshToken Create(string token, Guid userId, DateTime expires, DateTime created)
        {

            return new RefreshToken(token, userId, expires, created);
        }
    }
}