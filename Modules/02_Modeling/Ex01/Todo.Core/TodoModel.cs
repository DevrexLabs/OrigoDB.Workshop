using System;
using System.Collections.Generic;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class TodoModel : Model
    {
        public SortedDictionary<int, Todo> Todos { get; private set; }
        int _nextId = 1;

        public int GetNextId()
        {
            return _nextId++;
        }
        
        public TodoModel()
        {
            Todos = new SortedDictionary<int, Todo>();
        }
    }
    
    [Serializable]
    public class AddTodoCommand : Command<TodoModel, int>
    {
        public readonly string Title;

        public AddTodoCommand(string title)
        {
            Title = title;
        }

        public override int Execute(TodoModel model)
        {
            int id = model.GetNextId();
            model.Todos[id] = new Todo(id, Title);
            return id;
        }
    }

    [Serializable]
    public class SetCompletedCommand : Command<TodoModel>
    {
        public readonly int Id;
        public readonly DateTime Completed;

        public SetCompletedCommand(int id, DateTime completed)
        {
            Id = id;
            Completed = completed;
        }

        public override void Execute(TodoModel model)
        {
            if (!model.Todos.ContainsKey(Id)) Abort("No such todo");
            model.Todos[Id].SetCompleted(Completed);
        }
    }

}