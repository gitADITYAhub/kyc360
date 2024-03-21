using KYC360Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KYC360Api.Data
{
    public class MockDbContext
    {
        public List<Entity> Entities { get; set; }

        public MockDbContext()
        {
            Entities = new List<Entity>();

            // Predefined data for simplicity
            var firstNames = new[] { "John", "Jane", "Bob", "Alice", "Gary", "Eva", "Frank", "Nancy", "Oscar", "Lila" };
            var middleNames = new[] { "A.", "B.", "C.", "D.", "E.", "F.", "G.", "H.", "I.", "J." };
            var lastNames = new[] { "Doe", "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez" };
            var countries = new[] { "CountryA", "CountryB", "CountryC", "CountryD", "CountryE" };
            var cities = new[] { "CityA", "CityB", "CityC", "CityD", "CityE" };
            var addressLines = new[] { "123 Main St", "456 Maple Ave", "789 Oak St", "101 Pine Rd", "202 Birch St" };
            var dateTypes = new[] { "Birth", "Anniversary", "Other" };

            var random = new Random();

            // Add 10 random entities
            for (int i = 0; i < 10; i++)
            {
                Entities.Add(new Entity
                {
                    Id = Guid.NewGuid().ToString(),
                    Gender = random.Next(2) == 0 ? "Male" : "Female",
                    Deceased = random.Next(2) == 0,
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressLine = addressLines[random.Next(addressLines.Length)],
                            City = cities[random.Next(cities.Length)],
                            Country = countries[random.Next(countries.Length)]
                        }
                    },
                    Dates = new List<Date>
                    {
                        new Date
                        {
                            DateType = dateTypes[random.Next(dateTypes.Length)],
                            DateValue = DateTime.Now.AddDays(-random.Next(1, 10000)) // Random past date
                        }
                    },
                    Names = new List<Name>
                    {
                        new Name
                        {
                            FirstName = firstNames[random.Next(firstNames.Length)],
                            MiddleName = middleNames[random.Next(middleNames.Length)],
                            Surname = lastNames[random.Next(lastNames.Length)]
                        }
                    }
                });
            }
        }

        // CRUD Operations
        public IEnumerable<Entity> GetAllEntities()
        {
            return Entities;
        }

        public Entity GetEntityById(string id)
        {
            return Entities.FirstOrDefault(e => e.Id == id);
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public bool UpdateEntity(string id, Entity updatedEntity)
        {
            var index = Entities.FindIndex(e => e.Id == id);
            if (index == -1) return false;
            updatedEntity.Id = id; // Ensure the updated entity has the correct ID
            Entities[index] = updatedEntity;
            return true;
        }

        public bool DeleteEntity(string id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id == id);
            if (entity == null) return false;
            Entities.Remove(entity);
            return true;
        }
    }
}
