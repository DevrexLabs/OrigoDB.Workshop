#r Todo.Core/bin/debug/Todo.Core.dll

using Todo.Core;
using OrigoDB.Core;

var engine = Engine.LoadOrCreate<TodoModel>();
var command = new AddTodoCommand("Learn Scala");
int id = engine.Execute(command);

var todo = engine.Execute(db => db.Todos.Values.Where(t => t.Id == id).Single());

engine.Execute(new SetCompletedCommand(id, DateTime.Now));

var todo2 = engine.Execute(db => db.Todos[id]);