using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesInformationManager.Proxies;
using EmployeesInformationManager.Services;
using EmployeesInformationManager.Data;

namespace EmployeesInformationManager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DBServicesFacade _db;
        public EmployeeController(EmployeesInformationManagerContext context)
        {
            _db = new DBServicesFacade(context);
        }

        // GET: Employee
        public IActionResult Index()
        {
            return View(_db.GetNewModelView());
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,EmployeeSkills")] EmployeeModelView employeeModelView)
        {   
            if (ModelState.IsValid)
            {
                await _db.AddViewDataAsync(employeeModelView);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModelView);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Index",_db.GetNewModelView());
            }

            EmployeeModelView employeeModelView = _db.GetModelView(id.Value);
            if (employeeModelView == null)
            {
                return NotFound();
            }
            return View("Index",employeeModelView);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,EmployeeSkills")] EmployeeModelView employeeModelView)
        {
            if (id != employeeModelView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {        
                    await _db.UpdateFromViewAsync(employeeModelView);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_db.GetEmployee(employeeModelView.Id) != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModelView);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _db.GetEmployee(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _db.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}