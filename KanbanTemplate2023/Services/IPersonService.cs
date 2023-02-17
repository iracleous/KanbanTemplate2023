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

    
}
