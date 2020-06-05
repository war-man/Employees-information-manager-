using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Models;
using EmployeesInformationManager.Proxies;
using System.Collections.Generic;

namespace Employees_information_manager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeesInformationManagerContext _context;

        public EmployeeController(EmployeesInformationManagerContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            EmployeeModelView employeeModelView = new EmployeeModelView();
            FillSkills(employeeModelView);
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
                Employee employee = InsertEmployee(employeeModelView);
                _context.Add(employee);
                await _context.SaveChangesAsync();
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

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeModelView employeeModelView = new EmployeeModelView();
            var skillsNames = _context.EmployeeSkill
            .Where(es => es.EmployeeId == employee.Id)
            .Include(es => es.Skill)
            .Select(es => es.Skill.Name).ToList();

            employeeModelView.SetEmployee(employee,skillsNames);
            FillSkills(employeeModelView);
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
                     _context.EmployeeSkill.
                     RemoveRange(_context.EmployeeSkill
                     .Where(es => es.EmployeeId == employeeModelView.Id));
                    _context.SaveChanges();
                    Employee employee = InsertEmployee(employeeModelView);
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeModelView.Id))
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

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        private void FillSkills(EmployeeModelView employeeModelView)
        {
            List<string> skills = _context.Skill.Select(s => s.Name).ToList();
            var temp = string.Join(",",skills);
            employeeModelView.SuggestedSkills = "['"+temp.Replace(",", "','")+"']";
        }

        private Employee InsertEmployee(EmployeeModelView employeeModelView)
        {
            Employee employee = employeeModelView.ToEmployee();
            string cleanedString = employeeModelView.EmployeeSkills ?? "";
            string[] inputSkills = cleanedString.Split(',');
            foreach(string inputSkill in inputSkills)
            {
                Skill skill = _context.Skill
                .Where(s => s.Name == inputSkill)
                .FirstOrDefault();
                if(skill == null)
                {
                    skill = new Skill {Name = inputSkill};
                    _context.Skill.Add(skill);
                }
                
                EmployeeSkill employeeSkill = _context.EmployeeSkill
                .Where(es => es.SkillId == skill.Id
                && es.EmployeeId == employee.Id)
                .FirstOrDefault();
                if(employeeSkill == null)
                {
                    employeeSkill = new EmployeeSkill();
                    employeeSkill.EmployeeId = employee.Id;
                    employeeSkill.SkillId = skill.Id;
                    _context.EmployeeSkill.Add(employeeSkill);
                    employee.EmployeesSkills.Add(employeeSkill);
                    skill.EmployeesSkills.Add(employeeSkill); 
                }
            }
            return employee;
        }


    }
}
