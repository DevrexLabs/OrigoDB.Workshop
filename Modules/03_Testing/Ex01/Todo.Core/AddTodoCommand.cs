using System;
using OrigoDB.Core;

namespace Todo.Core
{
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
}