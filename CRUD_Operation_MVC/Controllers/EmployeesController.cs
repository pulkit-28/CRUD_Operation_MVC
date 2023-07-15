using CRUD_Operation_MVC.Data;
using CRUD_Operation_MVC.Models;
using CRUD_Operation_MVC.Models.Domian;
using CRUD_Operation_MVC.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Runtime.InteropServices;

namespace CRUD_Operation_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDBContext mvcDemoDBContext;

        public EmployeesController(MVCDemoDBContext mvcDemoDBContext)
        {
            this.mvcDemoDBContext = mvcDemoDBContext;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDBContext.Employees.ToListAsync();
            return View(employees);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Add(AddViewModel addEmployeeRequest)
        {
            bool containsNumeric = Numeric(addEmployeeRequest.Name);
              if(containsNumeric == true) 
            {
                return BadRequest("Name can not contain Numeric");
            }
              bool containsSpecialChar = Utility1.hasSpecialCharacter(addEmployeeRequest.Name);
            if(containsSpecialChar == true)
            {
                return BadRequest("Nmae can not Contain specilal Character");
            }
              

           if(string.IsNullOrWhiteSpace(addEmployeeRequest.Name))
            {
                return BadRequest();
            }
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DOB = addEmployeeRequest.DOB

            };

            await mvcDemoDBContext.Employees.AddAsync(employee);
            await mvcDemoDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
             
        }
        public static bool Numeric(string Name)
        {
            string numeric = @"0123456789";
            foreach(char item in numeric)
            {
                if (Name.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        
        [HttpGet]

        public async Task<IActionResult> View(Guid id)

        {
            var employee =  await mvcDemoDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DOB = employee.DOB
                };

                return await Task.Run(() => View("View",viewModel));
            
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {

            var employee = await mvcDemoDBContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DOB = model.DOB;

               await mvcDemoDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");



        }
        [HttpPost]

        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDBContext.Employees.FindAsync(model.Id);
            if(employee != null) 
            { 
                mvcDemoDBContext.Employees.Remove(employee);
                mvcDemoDBContext.SaveChanges();
                return RedirectToAction("Index");   
            }
            return RedirectToAction("Index");  

        }

    }
}
