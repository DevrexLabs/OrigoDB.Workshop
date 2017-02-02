using System;
using System.Collections.Generic;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class TodoModel : Model
    {
        public SortedDictionary<int, Todo> Todos { get; private set; }

        public TodoModel()
        {
            Todos = new SortedDictionary<int, Todo>();
        }
    }
}