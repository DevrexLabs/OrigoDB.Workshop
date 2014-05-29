#r Todo.Core\bin\debug\Todo.Core.dll

using Todo.Core;
using OrigoDB.Core;

var engine = Engine.LoadOrCreate<TodoModel>();

var haskell = engine.Execute(new AddTodoCommand("Learn Haskell"));
var cook = engine.Execute(new AddTodoCommand("Lamb Roast on Sunday"));
var dishes = engine.Execute(new AddTodoCommand("Do the dishes"));

engine.Execute(new SetCategoriesCommand{
   TodoId = haskell,
   Categories = new[]{"work", "play"}
});

engine.Execute(new SetCategoryCommand(cook,"play"));
engine.Execute(new SetCategoryCommand(dishes, "work"));