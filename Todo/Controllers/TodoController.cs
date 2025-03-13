using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    public class TodoController : Controller
    {

        private readonly ApplicationDbContext _db;

        public TodoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<TodoItem> list = _db.Todos.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem? obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Todos.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Check(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem todoObj = _db.Todos.FirstOrDefault(u => u.Id == id);

            if (todoObj == null)
            {
                return NotFound();
            }

            todoObj.IsCompleted = !todoObj.IsCompleted;

            _db.Todos.Update(todoObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Observe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem todoObj = _db.Todos.FirstOrDefault(u => u.Id == id);

            if (todoObj == null)
            {
                return NotFound();
            }

            return View(todoObj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            TodoItem todoObj = _db.Todos.FirstOrDefault(u => u.Id == id);

            if (todoObj == null)
            {
                return NotFound();
            }

            return View(todoObj);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem? obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Todos.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem todoObj = _db.Todos.FirstOrDefault(u => u.Id == id);

            if (todoObj == null)
            {
                return NotFound();
            }

            return View(todoObj);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteTodo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem todoObj = _db.Todos.FirstOrDefault(u => u.Id == id);

            if (todoObj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Todos.Remove(todoObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
