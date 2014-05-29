using System;
using System.Linq;

namespace Todo.Core
{
    [Serializable]
    public class TodoView
    {
        public readonly int Id;
        public readonly string Title;
        public readonly string[] Categories;
 
        public TodoView(Todo todo)
        {
            Id = todo.Id;
            Title = todo.Title;
            Categories = todo.Categories.Select(c => c.Name).ToArray();
        }
    }

}