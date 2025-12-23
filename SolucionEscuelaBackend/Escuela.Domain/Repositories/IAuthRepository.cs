using Escuela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Student> Login(string userName, string password);
    }
}
