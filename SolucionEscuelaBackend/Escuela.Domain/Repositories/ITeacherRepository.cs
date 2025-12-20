using Escuela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Domain.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAll();
        Task<Teacher> GetById(int id);
        Task<string> Insert(Teacher objTeacher);
        Task<bool> Update(Teacher objTeacher);
        Task<bool> Delete(int id);
    }
}
