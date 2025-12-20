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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly EscuelaDbContext _context;
        public TeacherRepository(EscuelaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Teacher>> GetAll()
        {
            return await _context.Teachers.Include(x => x.Subjects).Where(s => s.Active).ToListAsync();
        }

        public async Task<Teacher> GetById(int id)
        {
            return await _context.Teachers.Include(x => x.Subjects).FirstAsync(y => y.Id == id && y.Active);
        }

        public async Task<string> Insert(Teacher objTeacher)
        {
            try
            {
                await _context.Teachers.AddAsync(objTeacher);
                await _context.SaveChangesAsync();
                return objTeacher.Id.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<bool> Update(Teacher objTeacher)
        {
            try
            {
                _context.Entry(objTeacher).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Teacher objTeacher = await _context.Teachers.FirstAsync(x => x.Id == id);
                objTeacher.Active = false;
                _context.Entry(objTeacher).State = EntityState.Modified;
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
