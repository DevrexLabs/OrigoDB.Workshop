using System;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class GetTodoByIdQuery : Query<TodoModel, TodoView>
    {
        public readonly int Id;

        public GetTodoByIdQuery(int id)
        {
            Id = id;
        }

        public override TodoView Execute(TodoModel model)
        {
            return new TodoView(model.Todos[Id]);
        }
    }
}