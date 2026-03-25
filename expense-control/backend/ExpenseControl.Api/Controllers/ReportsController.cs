using ExpenseControl.Domain.Enums;
using ExpenseControl.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("persons")]
        public async Task<IActionResult> GetTotalsByPerson()
        {
            var persons = await _context.Persons
                .Include(p => p.Transactions)
                .ToListAsync();

            var result = persons.Select(p =>
            {
                var income = p.Transactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount);

                var expense = p.Transactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new
                {
                    Person = p.Name,
                    TotalIncome = income,
                    TotalExpense = expense,
                    Balance = income - expense
                };
            }).ToList();

            var totalIncome = result.Sum(r => r.TotalIncome);
            var totalExpense = result.Sum(r => r.TotalExpense);

            var summary = new
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = totalIncome - totalExpense
            };

            return Ok(new
            {
                Persons = result,
                Summary = summary
            });
        }
    }
}