namespace KanbanTemplate2023.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string ?Name { get; set; }
        public string ? Email { get; set; }
        public List<KanBanTask> Tasks { get; set; }
            = new List<KanBanTask>();
    }

    
}
