using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties
{
    public interface IPropertyService
    {
        Task<int> CreateAsync(Property newProperty);

        Task<int> DeleteAsync(int id);

        Task<bool> DoesExistAsync(int id);

        Task<List<Property>> GetAllAsync();

        Task<Property?> GetByIdAsync(int id);

        Task<Property?> UpdateAsync(Property updateProperty);
    }
}
