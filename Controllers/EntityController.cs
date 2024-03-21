using Microsoft.AspNetCore.Mvc;
using KYC360Api.Models;
using KYC360Api.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KYC360Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly MockDbContext _dbContext = new MockDbContext();
        // [HttpGet]
        // public IActionResult GetAllEntities()
        // {
        //     var entities = _dbContext.GetAllEntities();
        //     return Ok(entities);
        // }
        // GET: api/Entity
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> GetAllEntities(string search = null, string gender = null, DateTime? startDate = null, DateTime? endDate = null, string[] countries = null)
        {
            var query = _dbContext.Entities.AsQueryable();

            // Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => 
                    e.Names.Any(n => (n.FirstName + " " + n.MiddleName + " " + n.Surname).Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    e.Addresses.Any(a => a.AddressLine.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Country.Contains(search, StringComparison.OrdinalIgnoreCase)));
            }

            // Gender filter
            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(e => e.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));
            }

            // Date range filter
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(e => e.Dates.Any(d => d.DateValue >= startDate && d.DateValue <= endDate));
            }

            // Country filter
            if (countries != null && countries.Length > 0)
            {
                query = query.Where(e => e.Addresses.Any(a => countries.Contains(a.Country)));
            }

            return Ok(query.ToList());
        }

        // GET: api/Entity/5
        [HttpGet("{id}")]
        public ActionResult<Entity> GetEntityById(string id)
        {
            var entity = _dbContext.Entities.FirstOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // POST: api/Entity
        [HttpPost]
        public ActionResult<Entity> CreateEntity(Entity entity)
        {
            entity.Id = Guid.NewGuid().ToString(); // Generate new ID
            _dbContext.Entities.Add(entity);
            return CreatedAtAction(nameof(GetEntityById), new { id = entity.Id }, entity);
        }

        // PUT: api/Entity/5
        [HttpPut("{id}")]
        public IActionResult UpdateEntity(string id, Entity entity)
        {
            var index = _dbContext.Entities.FindIndex(e => e.Id == id);
            if (index < 0)
            {
                return NotFound();
            }

            entity.Id = id; // Ensure ID matches
            _dbContext.Entities[index] = entity;

            return NoContent();
        }

        // DELETE: api/Entity/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEntity(string id)
        {
            var index = _dbContext.Entities.FindIndex(e => e.Id == id);
            if (index < 0)
            {
                return NotFound();
            }

            _dbContext.Entities.RemoveAt(index);
            return NoContent();
        }
    }
}
