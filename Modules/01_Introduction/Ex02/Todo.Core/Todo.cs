using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace Todo.Core
{
    [Serializable]
    public class Todo
    {
        public readonly int Id;
        public string Title { get; private set; }
        public DateTime? Due { get; private set; }
        public DateTime? Completed { get; private set; }

        public Todo(int id)
        {
            Id = id;
        }
    }
}
