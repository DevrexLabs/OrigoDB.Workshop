using System;
using System.Collections.Generic;

namespace Todo.Core
{
    [Serializable]
    public class Category
    {
        public readonly string Name;
        public HashSet<Todo> Todos { get; private set; }

        public Category(string name)
        {
            Name = name;
            Todos = new HashSet<Todo>();
        }
    }
}