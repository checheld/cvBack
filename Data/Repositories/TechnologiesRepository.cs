#region Imports
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class TechnologiesRepository : ITechnologiesRepository
    {
        private ApplicationContext db;
        public TechnologiesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<List<TechnologyEntity>> AddTechnology(List<TechnologyEntity> technology)
        {
            try
            {
                await db.Technologies.AddRangeAsync(technology);
                await db.SaveChangesAsync();

                return technology;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteTechnologyById(int id)
        {
            try
            {
                db.Technologies.Remove(await db.Technologies.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TechnologyEntity>> GetAllTechnologies()
        {
            try
            {
                return await db.Technologies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TechnologyEntity> GetTechnologyById(int id)
        {
            try
            {
                return await db.Technologies.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return null;
            }
        }
    }
}