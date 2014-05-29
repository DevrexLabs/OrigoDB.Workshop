using System;
using System.Linq;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class PagedTodosQuery : Query<TodoModel, TodoView[]>
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public PagedTodosQuery()
        {
            Skip = 0;
            Take = 10;
        }

        public override TodoView[] Execute(TodoModel model)
        {
            return model.Todos.Values
                .Skip(Skip)
                .Take(Take)
                .Select(t => new TodoView(t))
                .ToArray();
        }
    }
}