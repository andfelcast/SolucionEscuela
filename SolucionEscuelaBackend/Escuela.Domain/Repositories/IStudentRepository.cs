using Escuela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<string> Register(Student objStudent);
        Task<bool> AddSubjects(int id, int[] subjectIds);
        Task<bool> Update(Student objStudent);
        Task<bool> Delete(int id);
    }
}
