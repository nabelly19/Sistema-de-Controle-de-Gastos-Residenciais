using System;

namespace ExpenseControl.Application.DTOs
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}