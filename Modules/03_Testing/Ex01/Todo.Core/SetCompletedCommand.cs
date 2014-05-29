using System;
using OrigoDB.Core;

namespace Todo.Core
{
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