using System;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class SetCategoriesCommand : Command<TodoModel>
    {
        public int TodoId { get; set; }
        public string[] Categories { get; set; }

        public override void Execute(TodoModel model)
        {
            if (!model.Todos.ContainsKey(TodoId)) Abort("No such todo");
            var todo = model.Todos[TodoId];
            foreach (var cat in model.Categories.Values) cat.Todos.Remove(todo);

            foreach (string categoryName in Categories)
            {
                model.SetCategory(TodoId, categoryName);
            }
        }
    }
}