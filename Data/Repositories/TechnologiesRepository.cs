using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public class TechnologiesRepository : ITechnologiesRepository
    {
        private ApplicationContext db;
        public TechnologiesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }
        public async Task<TechnologyEntity> AddTechnology(TechnologyEntity technology)
        {
            var foundTechnology = await db.Technologies.SingleOrDefaultAsync(x => x.Id == technology.Id);
            if (foundTechnology == null)
            {
                await db.Technologies.AddAsync(technology);
                await db.SaveChangesAsync();
                return technology;
            }
            return null;
        }

        public async Task<string> DeleteTechnologyById(int id)
        {
            var foundTechnology = await db.Technologies.SingleOrDefaultAsync(x => x.Id == id);
            if (foundTechnology != null)
            {
                db.Technologies.Remove(foundTechnology);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<TechnologyEntity>> GetAllTechnologies()
        {
            var technologies = await db.Technologies.ToListAsync();
            if (technologies != null)
            {
                return technologies;
            }
            return null;
        }

        public async Task<TechnologyEntity> GetTechnologyById(int id)
        {
            var technology = await db.Technologies.SingleOrDefaultAsync(x => x.Id == id);
            if (technology != null)
            {
                return technology;
            }
            return null;
        }

        public async Task<List<TechnologyEntity>> GetTechnologiesBySearch(string search)
        {
            try
            {
                return await db.Technologies
                    .Where(tech => tech.Name.Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public async Task<TechnologyEntity> UpdateTechnology(TechnologyEntity technology)
        {
            try
            {
                db.Technologies.Update(technology);
                await db.SaveChangesAsync();
                return technology;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}