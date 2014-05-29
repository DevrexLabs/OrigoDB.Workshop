using System;
using System.Linq;

namespace Todo.Core
{
    [Serializable]
    public class TodoView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Due { get; set; }
        public DateTime? Completed { get; set; }
        public readonly string[] Categories;

        public TodoView()
        {
            Categories = new string[0];
        }

        public TodoView(Todo todo)
        {
            Id = todo.Id;
            Title = todo.Title;
            Due = todo.Due;
            Completed = todo.Completed;
            Categories = todo.Categories.Select(c => c.Name).ToArray();
        }
    }

}