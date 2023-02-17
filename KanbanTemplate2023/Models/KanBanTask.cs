namespace KanbanTemplate2023.Models
{
    public class KanBanTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Person ?Person { get; set; }
      

    }
}
