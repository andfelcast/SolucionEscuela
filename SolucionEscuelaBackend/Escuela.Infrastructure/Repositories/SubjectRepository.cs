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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly EscuelaDbContext _context;
        public SubjectRepository(EscuelaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Subject>> GetAll()
        {
            return await _context.Subjects.Include(w => w.Teacher).Include(x => x.StudentXsubjects).ThenInclude(y => y.Student).Where(s => s.Active).ToListAsync();
        }

        public async Task<Subject> GetById(int id)
        {
            return await _context.Subjects.Include(w => w.Teacher).Include(x => x.StudentXsubjects).ThenInclude(y => y.Student).FirstAsync(s => s.Id == id);
        }

        public async Task<string> Insert(Subject objSubject)
        {
            try
            {
                await _context.Subjects.AddAsync(objSubject);
                await _context.SaveChangesAsync();
                return objSubject.Id.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<bool> Update(Subject objSubject)
        {
            try
            {
                _context.Entry(objSubject).State = EntityState.Modified;
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
                Subject objSubject = await _context.Subjects.FirstAsync(x => x.Id == id);
                objSubject.Active = false;
                _context.Entry(objSubject).State = EntityState.Modified;
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
