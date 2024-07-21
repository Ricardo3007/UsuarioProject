using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuarioProject.Application.DTO.Person;
using UsuarioProject.Application.Interfaces;

namespace UsuarioProject.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class PersonController : ControllerBase 
    { 
        private readonly IPersonService _personService; 

        public PersonController(IPersonService personService) 
        {
            _personService = personService; 
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PersonListDto>>> Get()
        {
            var personList = await _personService.Get();
            if (personList is null)
                return NoContent();

            return Ok(personList);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> Save([FromBody] PersonCreateDto person)
        {
            var productSaved = await _personService.Save(person);
            if (!productSaved)
                return BadRequest();

            return Ok(productSaved);
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<ActionResult<bool>> Update([FromBody] PersonUpdateDto person)
        {
            var updated = await _personService.Update(person);
            if (!updated)
                return BadRequest();

            return Ok(updated);
        }

        [HttpDelete("[action]/{id:int}")]
        [Authorize]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool deleted = await _personService.Delete(id);
            if (!deleted)
                return NoContent();

            return Ok(deleted);
        }
    } 
} 
