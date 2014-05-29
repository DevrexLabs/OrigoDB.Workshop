using System;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class SetCategoryCommand : Command<TodoModel>
    {
        public readonly int TodoId;
        public readonly string CategoryName;

        public SetCategoryCommand(int id, string category)
        {
            TodoId = id;
            CategoryName = category;
        }

        public override void Execute(TodoModel model)
        {
            //Validate
            if (!model.Todos.ContainsKey(TodoId)) Abort("No such todo");
            model.SetCategory(TodoId, CategoryName);
        }
    }
}