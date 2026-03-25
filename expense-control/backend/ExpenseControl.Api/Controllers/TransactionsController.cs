using ExpenseControl.Application.DTOs;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            var person = await _context.Persons.FindAsync(dto.PersonId);
            if (person == null)
                return NotFound("Pessoa não encontrada");

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                return NotFound("Categoria não encontrada");

            // REGRA 1: menor de idade só pode despesa
            if (person.Age < 18 && dto.Type == TransactionType.Income)
                return BadRequest("Menor de idade não pode ter receita");

            // REGRA 2: categoria compatível com tipo
            if (category.Purpose == CategoryPurpose.Expense && dto.Type == TransactionType.Income)
                return BadRequest("Categoria não permite receita");

            if (category.Purpose == CategoryPurpose.Income && dto.Type == TransactionType.Expense)
                return BadRequest("Categoria não permite despesa");

            var transaction = new Transaction(
                dto.Description,
                dto.Amount,
                dto.Type,
                dto.PersonId,
                dto.CategoryId
            );

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        // LIST
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _context.Transactions
                .Include(t => t.PersonId)
                .Include(t => t.CategoryId)
                .ToListAsync();

            return Ok(transactions);
        }
    }
}