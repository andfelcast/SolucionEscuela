using Escuela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Domain.Repositories
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAll();
        Task<Subject> GetById(int id);
        Task<string> Insert(Subject objSubject);
        Task<bool> Update(Subject objSubject);
        Task<bool> Delete(int id);
    }
}
