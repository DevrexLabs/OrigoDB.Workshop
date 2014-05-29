using System;
using System.Collections.Generic;
using OrigoDB.Core;

namespace Todo.Core
{
    [Serializable]
    public class TodoModel : Model
    {
        public SortedDictionary<int, Todo> Todos { get; private set; }
        public SortedDictionary<string, Category> Categories { get; private set; }
        int _nextId = 1;

        public int GetNextId()
        {
            return _nextId++;
        }
        
        public TodoModel()
        {
            Todos = new SortedDictionary<int, Todo>();
            Categories = new SortedDictionary<string, Category>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void SetCategory(int todoId, string categoryName)
        {
            //create the category if necessary
            if (!Categories.ContainsKey(categoryName))
                Categories[categoryName] = new Category(categoryName);

            var category = Categories[categoryName];
            var todo = Todos[todoId];

            //create the association
            category.Todos.Add(todo);
            todo.Categories.Add(category);
        }
    }
}