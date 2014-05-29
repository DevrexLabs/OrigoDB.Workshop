using System;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class RemoveTodoCommand : Command<TodoModel>
    {
        public readonly int Id;

        public RemoveTodoCommand(int id)
        {
            Id = id;
        }

        public override void Execute(TodoModel model)
        {
            if (!model.Todos.ContainsKey(Id)) return;
            var todo = model.Todos[Id];
            model.Todos.Remove(Id);
            foreach (var cat in model.Categories.Values) cat.Todos.Remove(todo);
        }
    }
}