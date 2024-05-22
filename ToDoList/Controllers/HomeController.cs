using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ToDoContext context;

        public HomeController(ILogger<HomeController> logger, ToDoContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<ToDo> query = context.LocalDB
                .Include(t => t.Category)
                .Include(t => t.Status);

            if (filters.HasCategory && int.TryParse(filters.CategoryId, out int categoryId))
            {
                query = query.Where(t => t.CategoryId == categoryId);
            }

            if (filters.HasStatus && int.TryParse(filters.StatusId, out int statusId))
            {
                query = query.Where(t => t.StatusId == statusId);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }
                else if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }
                else if (filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var status = context.Statuses.ToList();

            if (status == null || !status.Any())
            {
                _logger.LogError("Status is null or empty");
                return View("Error");
            }

            ViewBag.Status = status;
            var task = new ToDo { StatusId = 1 }; // Предполагая, что 1 представляет "открыто"
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            // Set ViewBag properties at the start of the method
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();

            if (ModelState.IsValid)
            {
                context.LocalDB.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            if (selected == null)
            {
                _logger.LogWarning("Получен null объект ToDo");
                return RedirectToAction("Index", new { ID = id });
            }

            var task = context.LocalDB.Find(selected.Id);

            if (task != null)
            {
                task.StatusId = 2; // Assuming 2 represents "closed"
                context.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Задача с ID {Id} не найдена", selected.Id);
            }

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.LocalDB.Where(t => t.StatusId == 2).ToList(); // Assuming 2 represents "closed"

            if (toDelete.Any())
            {
                foreach (var task in toDelete)
                {
                    context.LocalDB.Remove(task);
                }
                context.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Не найдено закрытых задач для удаления");
            }

            return RedirectToAction("Index", new { ID = id });
        }
    }
}