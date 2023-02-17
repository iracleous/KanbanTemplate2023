using KanbanTemplate2023.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KanbanTemplate2023.Services
{
    public interface IPersonService
    {

        public Task<List<Person>> GetAllAsync();
        public Task<Person?> GetByIdAsync(int ?id);
        public Task Save(Person person);
        public Task<bool> Update(Person person);
        public Task Delete(Person ?person);
        public bool IsAvailable();
    }

    public class PersonService : IPersonService
    {
        private readonly KanBanDbContext _context;

        public PersonService(KanBanDbContext context)
        {
            _context = context;
        }
        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }
        public async Task<Person?> GetByIdAsync(int ?id)
        {
            return await _context.Persons
                .Include(person => person.Tasks)
                .FirstOrDefaultAsync(m => m.PersonId == id)
            ;
        }
        public async Task Save(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Update(Person person)
        {

            try {  


            _context.Update(person);
            await _context.SaveChangesAsync();
            return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Delete(Person ?person)
        {
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
            await _context.SaveChangesAsync();
        }
        public bool IsAvailable()
        {
            return _context.Persons == null;
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }

    }
}
