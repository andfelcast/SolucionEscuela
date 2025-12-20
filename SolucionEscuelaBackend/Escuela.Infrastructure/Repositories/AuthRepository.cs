using Escuela.Domain.Repositories;
using Escuela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EscuelaDbContext _context;
        public AuthRepository(EscuelaDbContext context) { 
            _context = context;
        }
        public async Task<bool> Login(string userName, string password) {
            return await _context.Students.CountAsync(x => x.UserName == userName && x.Password == password) == 1;
        }
    }
}
