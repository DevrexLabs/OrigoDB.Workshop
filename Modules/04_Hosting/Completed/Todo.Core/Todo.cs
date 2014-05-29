using System;
using System.Collections.Generic;

namespace Todo.Core
{
    [Serializable]
    public class Todo
    {
        public DateTime? Due { get;  set; }
        public DateTime? Completed { get;  set; }
        public string Title { get;  set; }
        public HashSet<Category> Categories { get; private set; }
        public readonly int Id;

        public Todo(int id, string title = "Untitled")
        {
            Id = id;
            Title = title;
            Categories = new HashSet<Category>();
        }

        public void SetCompleted(DateTime completed)
        {
            Completed = completed;
        }
    }
}
