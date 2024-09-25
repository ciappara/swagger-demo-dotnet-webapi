using WebApiWithSwaggerDemo.Models;

namespace WebApiWithSwaggerDemo.Repositories
{
    public class TodoRepository
    {
        private static List<TodoTask> _tasks = new List<TodoTask>();


        public IEnumerable<TodoTask> GetAll() => _tasks;


        public TodoTask GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);


        public TodoTask Create(TodoTask task)
        {
            task.Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
            _tasks.Add(task);
            return task;
        }


        public TodoTask Complete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsComplete = true;
            }
            return task;
        }


        public bool Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return false;
            }

            _tasks.Remove(task);
            return true;
        }
    }
}
