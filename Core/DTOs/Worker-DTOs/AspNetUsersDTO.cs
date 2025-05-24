using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
        public class UserDto
        {
            public string Id { get; set; }               // Primary key
            public string FirstName { get; set; }        // Ім’я
            public string LastName { get; set; }         // Прізвище
            public string UserName { get; set; }         // Логін
            public string Email { get; set; }            // Електронна пошта
            public bool EmailConfirmed { get; set; }     // Чи підтверджена пошта
        public bool IsWorker { get; set; }           // Чи має роль "Worker" (не з таблиці напряму, а обчислюється)
        public List<string> Roles { get; set; } = new(); // ДОДАЙТЕ ЦЕ
    }
}
