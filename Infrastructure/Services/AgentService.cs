using Application.feature.Agents;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AgentService : IAgentService
    {       private readonly ApplicationDbContext _context;

        public AgentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Agent newAgent)
        {
            Console.WriteLine(newAgent);
            //We are working with Agent DB
            await _context.Agents.AddAsync(newAgent);
            //Apply change to the dtabase
           await _context.SaveChangesAsync();
            //return new agent with ID
            return newAgent.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {   //search for agent in the DB
           var agentInDB = await _context.Agents.FirstOrDefaultAsync(agent =>agent.Id == id);

            if(agentInDB != null)
            {   
                //this will put in que for removal
                _context.Agents.Remove(agentInDB);
                //Appy the changes in the database
                await _context.SaveChangesAsync();
                //Return the ID back to caller
                return agentInDB.Id;
            }
            return 0;
        }

        public async Task<bool> DoesExistAsync(int id)
        {   
            //Find any match to the given ID this return true or false
           return  await _context.Agents.AnyAsync(agent => agent.Id == id);
        }

        public Task<List<Agent>> GetAllAsync()
        {   //this will return a list
            return _context.Agents.ToListAsync();
        }

        public async Task<Agent> GetByIdAsync(int id)
        {
            var agentInDb = await _context.Agents.FirstOrDefaultAsync(agent=> agent.Id == id);
            if (agentInDb != null) { 
                return agentInDb;            
            }

            return null;
        }

        public async Task<Agent> UpdateAsync(Agent updateAgent)
        {
            var agentInDb = await DoesExistAsync(updateAgent.Id);

           if (agentInDb)
            {
                _context.Agents.Update(updateAgent);
                //save 

                await _context.SaveChangesAsync();
                return updateAgent;
            }
            return null;
        }
    }
}
