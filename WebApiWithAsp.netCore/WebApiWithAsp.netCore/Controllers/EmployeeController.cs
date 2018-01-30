using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiWithAsp.netCore.Model;

namespace WebApiWithAsp.netCore.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        // GET api/values
        [HttpGet]
        public List<Employee> Get()
        {
            using (var dbContext = new NorthwindDbContext())
            {
                return dbContext.Employees.ToList();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            using(var dbContext = new NorthwindDbContext())
            {
                return dbContext.Employees.Find(id);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Employee obj)
        {
            using (var dbContext = new NorthwindDbContext())
            {
                dbContext.Employees.Add(obj);
                dbContext.SaveChanges();
                return new ObjectResult("Employee added successfully!");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee obj)
        {
            using (NorthwindDbContext db = new NorthwindDbContext())
            {
                db.Entry<Employee>(obj).State = EntityState.Modified;
                db.SaveChanges();
                return new ObjectResult("Employee modified successfully!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (NorthwindDbContext db = new NorthwindDbContext())
            {

                db.Employees.Remove(db.Employees.Find(id));
                db.SaveChanges();
                return new ObjectResult("Employee deleted successfully!");
            }
        }
    }
}

