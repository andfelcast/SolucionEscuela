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
                await _context.Students.AddAsync(objStudent);
                await _context.SaveChangesAsync();
                return objStudent.UserName;
            }
            catch (Exception) {
                return string.Empty;
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
