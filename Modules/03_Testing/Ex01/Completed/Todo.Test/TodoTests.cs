
using System;
using NUnit.Framework;
using OrigoDB.Core;
using OrigoDB.Core.Test;
using Todo.Core;


namespace Todo.Test
{
    [TestFixture]
    public class TodoTests
    {
        [Test]
        public void Todo_Constructor_args_are_picked_up()
        {
            const int expectedId = 42;
            const string expectedTodo = "Give the dog a dose of meth";

            var todo = new Core.Todo(expectedId, expectedTodo);
            Assert.AreEqual(expectedId, todo.Id);
            Assert.AreEqual(expectedTodo, todo.Title);
        }

        [Test]
        public void SetCompletedCommand_marks_todo_completed()
        {
            var timestamp = DateTime.Now;

            //arrange
            var model = new TodoModel();
            var todo = new Core.Todo(1, "Bury the dog");
            model.Todos[1] = todo; 

            //act
            var cmd = new SetCompletedCommand(1, timestamp);
            cmd.Execute(model);

            //assert
            Assert.IsTrue(todo.Completed.HasValue);
            Assert.AreEqual(timestamp, todo.Completed.Value);
        }

        [Test]
        public void SetCategory_creates_a_category()
        {
            const string category = "Hobbies";
            var target = new TodoModel();
            target.Todos.Add(1, new Core.Todo(1, "Buy a puppy"));
            target.SetCategory(1, category);
            Assert.IsTrue(target.Categories.ContainsKey(category));
        }

        [Test]
        public void Smoke_test()
        {
            const string category = "Hobbies";
            const string title = "Eat";
            var config = new EngineConfiguration().ForIsolatedTest();
            var engine = Engine.For<TodoModel>(config);
            var id = engine.Execute(new AddTodoCommand(title));
            engine.Execute(new SetCategoryCommand(id, category));
            TodoView todo = engine.Execute(new GetTodoByIdQuery(id));
            Assert.AreEqual(title, todo.Title);
            Assert.AreEqual(id,todo.Id);
            Assert.AreEqual(1, todo.Categories.Length);
            Assert.AreEqual(category, todo.Categories[0]);
        }
    }
}
