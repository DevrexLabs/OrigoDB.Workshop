
//1. Category names
> engine.Execute(m => m.Categories.Keys)
[
   "play",
   "work"
]

//2. Todo item names in alphabetical order
> engine.Execute(m => m.Todos.Values.OrderBy(t => t.Title).Select(t => t.Title).ToArray())
[
  "Do the dishes",
  "Lamb Roast on Sunday",
  "Learn Haskell"
]


//3. Unfinished todo items
> engine.Execute(m => m.Todos.Values.Where(t => !t.Completed.HasValue).ToArray())
[]

//4. Overdue todo items
> engine.Execute(m => m.Todos.Values.Where(t => !t.Completed.HasValue && DateTime.Now > t.Due).ToArray())
[]