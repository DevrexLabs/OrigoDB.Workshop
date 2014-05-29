using System.Data;
using System.Web.Mvc;
using OrigoDB.Core;
using Todo.Core;

namespace Todo.Mvc.Controllers
{
    public class TodoController : Controller
    {
        private IEngine<TodoModel> engine = Engine.For<TodoModel>();

        //
        // GET: /Todo/

        public ActionResult Index()
        {
            var todos = engine.Execute(new PagedTodosQuery());
            return View(todos);
        }

        //
        // GET: /Todo/Details/5

        public ActionResult Details(int id = 0)
        {
            var todo = engine.Execute(new GetTodoByIdQuery(id));
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // GET: /Todo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Todo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoView todo)
        {
            if (!string.IsNullOrEmpty(todo.Title))
            {
                int id = engine.Execute(new AddTodoCommand(todo.Title){Due = todo.Due});
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        //
        // GET: /Todo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var todo = engine.Execute(new GetTodoByIdQuery(id));
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // POST: /Todo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TodoView todo)
        {
            if (ModelState.IsValid)
            {
                var cmd = new UpdateTodoCommand() {Source = todo};
                engine.Execute(cmd);
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        //
        // GET: /Todo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var todo = engine.Execute(new GetTodoByIdQuery(id));
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // POST: /Todo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            engine.Execute(new RemoveTodoCommand(id));
            return RedirectToAction("Index");
        }
    }
}