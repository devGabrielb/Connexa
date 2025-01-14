using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);

        public bool IsCorrectPassword(string password, string hash);
    }
}