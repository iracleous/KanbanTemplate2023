using Microsoft.EntityFrameworkCore;

namespace KanbanTemplate2023.Models
{
    public class KanBanDbContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<KanBanTask> Tasks { get; set; }

        public KanBanDbContext(
        DbContextOptions<KanBanDbContext> 
        options) : base(options)
    { }

}

}
