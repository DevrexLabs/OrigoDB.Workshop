using System;
using System.Linq;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class UpdateTodoCommand : Command<TodoModel>
    {
        public TodoView Source { get; set; }

        public override void Execute(TodoModel model)
        {
            if (!model.Todos.ContainsKey(Source.Id)) Abort("No such todo");
            var todo = model.Todos[Source.Id];
            todo.Title = Source.Title;
            todo.Completed = Source.Completed;
            todo.Due = Source.Due;
        }
    }
}