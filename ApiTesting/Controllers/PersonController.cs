using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPersons(int quantity)
        {
            var person = Person.GeneratePersons(quantity);
            Console.WriteLine($"recebi requisicao");
            return Ok(person);
        }
    }
}