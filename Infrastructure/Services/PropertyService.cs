using Application.feature.Properties;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly ApplicationDbContext _context;

        public PropertyService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new property
        public async Task<int> CreateAsync(Property newProperty)
        {
            // Add the property to EF Core
            await _context.Properties.AddAsync(newProperty);

            // Save the property to the database
            await _context.SaveChangesAsync();

            // Return the generated ID
            return newProperty.Id;
        }

        // Delete a property
        public async Task<int> DeleteAsync(int id)
        {
            // Find the property
            var propertyInDb = await _context.Properties
                .FirstOrDefaultAsync(property => property.Id == id);

            // Check if the property exists
            if (propertyInDb != null)
            {
                // Mark property for deletion
                _context.Properties.Remove(propertyInDb);

                // Save changes
                await _context.SaveChangesAsync();

                // Return the deleted property's ID
                return propertyInDb.Id;
            }

            // Property was not found
            return 0;
        }

        // Check if a property exists
        public async Task<bool> DoesExistAsync(int id)
        {
            return await _context.Properties
                .AnyAsync(property => property.Id == id);
        }

        // Get all properties
        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(property => property.Agent)
                .ToListAsync();
        }

        // Get one property
        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties.Include(property => property.Agent).FirstOrDefaultAsync(property => property.Id == id);
        }

        // Update a property
        public async Task<Property?> UpdateAsync(Property updateProperty)
        {
            // Check if property exists
            var propertyExists = await DoesExistAsync(updateProperty.Id);

            if (!propertyExists)
            {
                return null;
            }

            // Mark property as modified
            _context.Properties.Update(updateProperty);

            // Save changes
            await _context.SaveChangesAsync();

            return updateProperty;
        }
    }
}