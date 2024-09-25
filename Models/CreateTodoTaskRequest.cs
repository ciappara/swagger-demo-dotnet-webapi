using System.ComponentModel.DataAnnotations;

namespace WebApiWithSwaggerDemo.Models
{
    public class CreateTodoTaskRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
