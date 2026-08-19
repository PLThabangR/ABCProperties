using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Agents
{
    public interface IAgentService
    {   
        //***************This are the Commands*****************//
        //The method will return a int primary key of the created agentcdc       
         Task<int> CreateAsync(Agent newAgent);
        //We will return a updated agent
        Task<Agent> UpdateAsync(Agent updateAgent);

        //We return the primary key of the deleted
        Task<int> DeleteAsync(int id);


        //**************This are the Queries****************//
        Task<Agent> GetByIdAsync(int id);
        Task<List<Agent>> GetAllAsync();

        //Helper method
        Task<bool> DoesExistAsync(int id);
    }
}
