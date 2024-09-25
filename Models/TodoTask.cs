namespace WebApiWithSwaggerDemo.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
    }
}
