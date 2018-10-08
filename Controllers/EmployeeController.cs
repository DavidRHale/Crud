using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Microsoft.AspNetCore.Mvc;
using MVCAdoDemo.Models;

namespace MVCAdoDemo.Controllers
{
    public class EmployeeController : Controller
    {
        IDocumentStore _store;

        public EmployeeController(IDocumentStore documentStore)
        {
            _store = documentStore;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> lstEmployee;

            using (var session = _store.QuerySession())
            {
                lstEmployee = session.Query<Employee>();
            }

            // Check not empty, catch exceptions            

            return View(lstEmployee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var session = _store.LightweightSession())
                {
                    session.Insert(employee);
                    session.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee;
            using (var session = _store.QuerySession())
            {
                employee = session.Load<Employee>(id.Value);
            }

            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind]Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (var session = _store.LightweightSession())
                {
                    session.Update(employee);
                    session.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee;
            using (var session = _store.QuerySession())
            {
                employee = session.Load<Employee>(id.Value);
            }

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Employee employee;
            using (var session = _store.QuerySession())
            {
                employee = session.Load<Employee>(id.Value);
            }

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            using (var session = _store.LightweightSession())
            {
                session.Delete<Employee>(id.Value);
                session.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}