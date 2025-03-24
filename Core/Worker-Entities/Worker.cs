using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int LocationId { get; set; }
        public int? BryhadyrId { get; set; } // FK до бригадира
        public int PositionId { get; set; }
        public int BossId { get; set; }
        public double? Latitute { get; set; }
        public double? Longitute { get; set; }

        // Навігаційні властивості (один об'єкт)
        public Location Location { get; set; }
        public Position Position { get; set; }
        public Boss Boss { get; set; }
        public Worker Bryhadyr { get; set; } // Один бригадир

        // Навігаційні властивості (колекції)
        public ICollection<Worker> SubWorkers { get; set; } // Працівники, підлеглі бригадиру
        public ICollection<PowerTool> PowerTools { get; set; }
        public ICollection<HandTool> HandTools { get; set; }
        public ICollection<Batary> Bataries { get; set; }
    }
}
