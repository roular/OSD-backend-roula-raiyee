using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        public IConfiguration _configuration;
        private PersonContext _personContext;
        public PersonsController(IConfiguration config, PersonContext context)
        {
            _configuration = config;
            _personContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await _personContext.Persons.ToListAsync();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Person person)
        {

            if (person == null)
            {
                return NotFound("Person data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Person? existingPerson = _personContext.Persons.FirstOrDefault(p => p.Email == person.Email);
            if (existingPerson == null)
            {
                return NotFound("Person does not exist in the database");
            }
            if (!BC.Verify(person.Password, existingPerson.Password))
            {
                return BadRequest("Wrong Username-Password matching");
            }
            return Json(new { email = existingPerson.Email, id = existingPerson.Personid });
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Person person)
        {
            if (person == null)
            {
                return NotFound("Person data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Person? existingPerson = _personContext.Persons.FirstOrDefault(p => p.Personid == person.Personid);
            if (existingPerson == null)
            {
                return NotFound("Student does not exist in the database");
            }
            existingPerson.Email = person.Email;
            existingPerson.Password = person.Password;
            _personContext.Attach(existingPerson).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _personContext.SaveChangesAsync();
            return Ok(existingPerson);
        }

        [HttpPost]
        public async Task<ActionResult> GetPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return NotFound("Person data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            person.Password = BC.HashPassword(person.Password);
            _personContext.Persons.Add(person);
            await _personContext.SaveChangesAsync();
            return Json(new { result = "Person Created Successfully" });

        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteTodoItem([FromBody] Person person)
        {
            Person? existingPerson = _personContext.Persons.Where(p => p.Email == person.Email).FirstOrDefault();
            if (existingPerson == null)
            {
                return NotFound();
            }

            _personContext.Persons.Remove(existingPerson);
            await _personContext.SaveChangesAsync();
            return Json(new { result = "Person Deleted Successfully" });

        }

        ~PersonsController()
        {
            _personContext.Dispose();
        }

    }
}
