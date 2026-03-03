using Microsoft.AspNetCore.Mvc;
using restAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace restAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        // in‑memory list seeded with three records
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR" },
            new Employee { Id = 2, Name = "Bob", Department = "IT" },
            new Employee { Id = 3, Name = "Charlie", Department = "Finance" }
        };

        [HttpGet]
        public IEnumerable<Employee> Get() => _employees;

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            return emp;
        }

        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            if (employee == null) return BadRequest();

            var maxId = _employees.Any() ? _employees.Max(e => e.Id) : 0;
            employee.Id = maxId + 1;
            _employees.Add(employee);

            return CreatedAtAction(nameof(GetById),
                                   new { id = employee.Id },
                                   employee);
        }
    }
}