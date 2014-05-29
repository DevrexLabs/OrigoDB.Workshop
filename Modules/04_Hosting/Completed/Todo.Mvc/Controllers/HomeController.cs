using System.Web.Mvc;

    //namespace Todo.Core
    //{
    //    public class Todo
    //    {
    //        public int Id { get; set; }
    //        public string Title { get; set; }
    //        public DateTime? Due { get; set; }
    //        public DateTime? Completed { get; set; }
    //    }

    //    public class TodoContext : DbContext
    //    {
    //        public DbSet<Todo> Todos { get; set; }
    //    }
    //}

namespace Todo.Mvc.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
