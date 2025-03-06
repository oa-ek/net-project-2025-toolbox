using ToolTrack.Repository.Repos;
using Core;
using System.Collections.Generic;
using ToolTrack.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace SeedData
{
    public class Seed
    {
        public static async Task InitializeAsync(RepositoryContainer repositories)
        {
            // Brands
            if (!await repositories.BrandRepository.AnyAsync())
            {
                await repositories.BrandRepository.AddRangeAsync(new List<Brand>
                {
                    new Brand { Name = "Brand A" },
                    new Brand { Name = "Brand B" },
                    new Brand { Name = "Brand C" }
                });
            }

            // Battery Models
            if (!await repositories.BataryModelRepository.AnyAsync())
            {
                await repositories.BataryModelRepository.AddRangeAsync(new List<BataryModel>
                {
                    new BataryModel { Name = "Model X", BrandId = 1 },
                    new BataryModel { Name = "Model Y", BrandId = 2 },
                    new BataryModel { Name = "Model Z", BrandId = 3 }
                });
            }

            // Conditions
            if (!await repositories.ConditionRepository.AnyAsync())
            {
                await repositories.ConditionRepository.AddRangeAsync(new List<Condition>
                {
                    new Condition { Name = "New" },
                    new Condition { Name = "Used" },
                    new Condition { Name = "Damaged" }
                });
            }

            // Locations
            if (!await repositories.LocationRepository.AnyAsync())
            {
                var locations = new List<Location>
                {
                    new Location { Name = "Warehouse A", Description = "Main storage facility", Latitute = 50.4501, Longitute = 30.5234 },
                    new Location { Name = "Warehouse B", Description = "Secondary storage facility", Latitute = 48.3794, Longitute = 31.1656 },
                    new Location { Name = "Office", Description = "Corporate headquarters", Latitute = 49.8397, Longitute = 24.0297 }
                };
                await repositories.LocationRepository.AddRangeAsync(locations);
                await repositories.LocationRepository.SaveChangesAsync();
            }

            // Bosses
            if (!await repositories.BossRepository.AnyAsync())
            {
                var bosses = new List<Boss>
                {
                    new Boss { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "555-1234", Password = "password1" },
                    new Boss { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "555-5678", Password = "password2" },
                    new Boss { FirstName = "Alex", LastName = "Taylor", Email = "alex.taylor@example.com", Phone = "555-9876", Password = "password3" }
                };
                await repositories.BossRepository.AddRangeAsync(bosses);
                await repositories.BossRepository.SaveChangesAsync();
            }

            // Positions
            if (!await repositories.PositionRepository.AnyAsync())
            {
                var positions = new List<Position>
                {
                    new Position { Name = "Technician", SalaryPerHour = 150, BossId = 1 },
                    new Position { Name = "Manager", SalaryPerHour = 200, BossId = 2 },
                    new Position { Name = "Engineer", SalaryPerHour = 180, BossId = 3 }
                };
                await repositories.PositionRepository.AddRangeAsync(positions);
                await repositories.PositionRepository.SaveChangesAsync();
            }

            // Workers
            if (!await repositories.WorkerRepository.AnyAsync())
            {
                var workers = new List<Worker>
                {
                    new Worker { FirstName = "Ivan", LastName = "Petrov", Email = "ivan.petrov@example.com", Phone = "123456789", Password = "pass333", BossId = 1, LocationId = 1, PositionId = 1 },
                    new Worker { FirstName = "Anna", LastName = "Ivanova", Email = "anna.ivanova@example.com", Phone = "987654321", Password = "pass222", BossId = 2, LocationId = 2, PositionId = 2 },
                    new Worker { FirstName = "Serhiy", LastName = "Bondarenko", Email = "serhiy.bond@example.com", Phone = "456123789", Password = "pass111", BossId = 3, LocationId = 3, PositionId = 3 }
                };
                await repositories.WorkerRepository.AddRangeAsync(workers);
                await repositories.WorkerRepository.SaveChangesAsync();

                // Отримуємо фактичні Id доданих працівників
                var ivan = await repositories.WorkerRepository.FirstOrDefaultAsync(w => w.Email == "ivan.petrov@example.com");
                var anna = await repositories.WorkerRepository.FirstOrDefaultAsync(w => w.Email == "anna.ivanova@example.com");
                var serhiy = await repositories.WorkerRepository.FirstOrDefaultAsync(w => w.Email == "serhiy.bond@example.com");

                // Power Tools
                if (!await repositories.PowerToolRepository.AnyAsync())
                {
                    var powerTools = new List<PowerTool>
                    {
                        new PowerTool { TypeId = 1, ConditionId = 1, LastWorkerId = ivan.Id, LastLocationId = 1, ToolModelId = 1, HaveCase = true, DateMade = new DateOnly(2021, 7, 20), SerialNumber = "PT12345", Number = "PT001", Price = 150, PowerSupplyTypeId = 1 },
                        new PowerTool { TypeId = 2, ConditionId = 2, LastWorkerId = anna.Id, LastLocationId = 2, ToolModelId = 2, HaveCase = false, DateMade = new DateOnly(2020, 12, 15), SerialNumber = "PT67890", Number = "PT002", Price = 120, PowerSupplyTypeId = 2 },
                        new PowerTool { TypeId = 3, ConditionId = 3, LastWorkerId = serhiy.Id, LastLocationId = 3, ToolModelId = 3, HaveCase = true, DateMade = new DateOnly(2019, 4, 10), SerialNumber = "PT54321", Number = "PT003", Price = 100, PowerSupplyTypeId = 3 }
                    };
                    await repositories.PowerToolRepository.AddRangeAsync(powerTools);
                    await repositories.PowerToolRepository.SaveChangesAsync();
                }

                // Work Statistics
                if (!await repositories.WorkStatisticRepository.AnyAsync())
                {
                    await repositories.WorkStatisticRepository.AddRangeAsync(new List<WorkStatistic>
                    {
                        new WorkStatistic { WorkerId = ivan.Id, Date = new DateOnly(2023, 1, 15), HoursWorked = 8, LocationId = 1, Submiteed = true },
                        new WorkStatistic { WorkerId = anna.Id, Date = new DateOnly(2023, 1, 16), HoursWorked = 6, LocationId = 2, Submiteed = true },
                        new WorkStatistic { WorkerId = serhiy.Id, Date = new DateOnly(2023, 1, 17), HoursWorked = 7, LocationId = 3, Submiteed = true }
                    });
                    await repositories.WorkStatisticRepository.SaveChangesAsync();
                }

                // Batteries
                if (!await repositories.BateryRepository.AnyAsync())
                {
                    await repositories.BateryRepository.AddRangeAsync(new List<Batary>
                    {
                        new Batary
                        {
                            BataryModelId = 1,
                            DateMade = new DateOnly(2023, 3, 15),
                            SerialNumber = "BAT001",
                            Number = "B123",
                            Price = 99.99,
                            ConditionId = 1,
                            LastWorkerId = ivan.Id,
                            LastLocationId = 1
                        },
                        new Batary
                        {
                            BataryModelId = 2,
                            DateMade = new DateOnly(2022, 6, 20),
                            SerialNumber = "BAT002",
                            Number = "B124",
                            Price = 89.50,
                            ConditionId = 2,
                            LastWorkerId = null,
                            LastLocationId = 2
                        },
                        new Batary
                        {
                            BataryModelId = 3,
                            DateMade = new DateOnly(2021, 10, 10),
                            SerialNumber = "BAT003",
                            Number = "B125",
                            Price = 75.00,
                            ConditionId = 3,
                            LastWorkerId = serhiy.Id,
                            LastLocationId = 3
                        }
                    });
                    await repositories.BateryRepository.SaveChangesAsync();
                }

                // Hand Tools
                if (!await repositories.HandToolRepository.AnyAsync())
                {
                    await repositories.HandToolRepository.AddRangeAsync(new List<HandTool>
                    {
                        new HandTool
                        {
                            BrandId = 1,
                            ConditionId = 1,
                            ToolTypeId = 1,
                            LastWorkerId = ivan.Id,
                            LastLocationId = 1,
                            Price = 49.99
                        },
                        new HandTool
                        {
                            BrandId = 2,
                            ConditionId = 2,
                            ToolTypeId = 2,
                            LastWorkerId = anna.Id,
                            LastLocationId = 2,
                            Price = 29.99
                        },
                        new HandTool
                        {
                            BrandId = 3,
                            ConditionId = 3,
                            ToolTypeId = 3,
                            LastWorkerId = null,
                            LastLocationId = 3,
                            Price = 39.99
                        }
                    });
                    await repositories.HandToolRepository.SaveChangesAsync();
                }
            }

            // Tool Types
            if (!await repositories.ToolTypeRepository.AnyAsync())
            {
                var toolTypes = new List<ToolType>
                {
                    new ToolType { Name = "Drill" },
                    new ToolType { Name = "Hammer" },
                    new ToolType { Name = "Saw" }
                };
                await repositories.ToolTypeRepository.AddRangeAsync(toolTypes);
                await repositories.ToolTypeRepository.SaveChangesAsync();
            }

            // Power Supply Types
            if (!await repositories.PowerSupplyTypeRepository.AnyAsync())
            {
                await repositories.PowerSupplyTypeRepository.AddRangeAsync(new List<PowerSupplyType>
                {
                    new PowerSupplyType { Name = "Battery-powered" },
                    new PowerSupplyType { Name = "Corded" },
                    new PowerSupplyType { Name = "Manual" }
                });
            }

            // Tool Models
            if (!await repositories.ToolModelRepository.AnyAsync())
            {
                await repositories.ToolModelRepository.AddRangeAsync(new List<ToolModel>
                {
                    new ToolModel { Name = "Model A", BrandId = 1 },
                    new ToolModel { Name = "Model B", BrandId = 2 },
                    new ToolModel { Name = "Model C", BrandId = 3 }
                });
            }

            // System Admins
            if (!await repositories.SystemAdminRepository.AnyAsync())
            {
                await repositories.SystemAdminRepository.AddRangeAsync(new List<SystemAdmin>
                {
                    new SystemAdmin { FirstName = "Mark", LastName = "Johnson", Email = "mark.johnson@example.com", Phone = "555-0001", Password = "adminpass1", BossId = 1 },
                    new SystemAdmin { FirstName = "Emily", LastName = "Clark", Email = "emily.clark@example.com", Phone = "555-0002", Password = "adminpass2", BossId = 2 },
                    new SystemAdmin { FirstName = "Chris", LastName = "Evans", Email = "chris.evans@example.com", Phone = "555-0003", Password = "adminpass3", BossId = 3 }
                });
            }
        }
    }
}


