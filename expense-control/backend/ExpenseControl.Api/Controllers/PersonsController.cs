using ExpenseControl.Application.DTOs;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonDto dto)
        {
            var person = new Person(dto.Name, dto.Age);

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _context.Persons.ToListAsync();
            return Ok(persons);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreatePersonDto dto)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();

            person.GetType().GetProperty("Name")?.SetValue(person, dto.Name);
            person.GetType().GetProperty("Age")?.SetValue(person, dto.Age);

            await _context.SaveChangesAsync();

            return Ok(person);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var person = await _context.Persons
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound();

            // Regra: remover transações junto
            _context.Transactions.RemoveRange(person.Transactions);
            _context.Persons.Remove(person);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}