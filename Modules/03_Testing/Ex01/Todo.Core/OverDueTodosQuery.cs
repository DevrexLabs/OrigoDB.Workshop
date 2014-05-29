using System;
using System.Linq;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class OverDueTodosQuery : Query<TodoModel, TodoView[]>
    {
        public override TodoView[] Execute(TodoModel model)
        {
            var query =
                from todo in model.Todos.Values
                where !todo.Completed.HasValue && DateTime.Now > todo.Due
                select new TodoView(todo);
                     
            return query.ToArray();
        }
    }
}