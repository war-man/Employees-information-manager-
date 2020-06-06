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
        private readonly EmployeeServices _employeeServices;
        private readonly SkillServices _skillServices;

        public EmployeeController(EmployeesInformationManagerContext context)
        {
            _employeeServices = new EmployeeServices(context);
            _skillServices = new SkillServices(context);
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _employeeServices.GetAll());
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            EmployeeModelView employeeModelView = new EmployeeModelView();
            employeeModelView.SuggestedSkills = _skillServices.GetAllAsArrayString();
            return View(employeeModelView);
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
                await _employeeServices.Add(employeeModelView);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModelView);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeServices.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeModelView employeeModelView = new EmployeeModelView();
            employeeModelView.SetEmployee(employee,_employeeServices.GetSkillsNames(id));
            employeeModelView.SuggestedSkills = _skillServices.GetAllAsArrayString();
            return View(employeeModelView);
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
                    await _employeeServices.Update(employeeModelView);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_employeeServices.Exists(employeeModelView.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeServices.Get(id);
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
            await _employeeServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
