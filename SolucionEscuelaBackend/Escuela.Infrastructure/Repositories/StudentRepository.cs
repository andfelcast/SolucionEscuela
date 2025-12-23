using Escuela.Domain.Entities;
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
    public class StudentRepository : IStudentRepository
    {
        private readonly EscuelaDbContext _context;
        public StudentRepository(EscuelaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAll() {
            return await _context.Students.Include(x => x.StudentXsubjects).ThenInclude(y => y.Subject).Where(s => s.Active).ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Students.Include(x => x.StudentXsubjects).ThenInclude(y => y.Subject).FirstAsync(w => w.Id == id && w.Active);
        }

        public async Task<string> Register(Student objStudent) {
            try
            {
                objStudent.Active = true;
                objStudent.CreationDate = DateTime.Now;
                objStudent.UserName = (objStudent.FirstName.Substring(0, 1) + objStudent.LastName.Split(" ")[0]).ToLower();
                await _context.Students.AddAsync(objStudent);
                await _context.SaveChangesAsync();
                return objStudent.UserName;
            }
            catch (Exception) {
                return string.Empty;
            }
            
        }

        public async Task<bool> AddSubjects(int id, int[] subjectIds) {
            try
            {

                List<StudentXsubject> lstSubjects = await _context.StudentXsubjects.Where(x => x.StudentId == id && x.Active).ToListAsync();
                foreach (var item in lstSubjects)
                {
                    if (!subjectIds.Contains(item.SubjectId))
                    {
                        item.Active = false;
                        _context.Entry(item).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                for (int i = 0; i < subjectIds.Length; i++)
                {

                    if (lstSubjects.Count(x => x.SubjectId == subjectIds[i] && x.StudentId == id) == 0)
                    {
                        StudentXsubject newSubject = new StudentXsubject
                        {
                            CreationDate = DateTime.Now,
                            Active = true,
                            StudentId = id,
                            SubjectId = subjectIds[i],
                        };
                        await _context.StudentXsubjects.AddAsync(newSubject);
                        await _context.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Update(Student objStudent) {
            try
            {
                _context.Entry(objStudent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Student objStudent = await _context.Students.FirstAsync(x => x.Id == id);
                objStudent.Active = false;
                _context.Entry(objStudent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
