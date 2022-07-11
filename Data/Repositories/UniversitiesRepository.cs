#region Imports
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class UniversitiesRepository : IUniversitiesRepository
    {
        private ApplicationContext db;
        public UniversitiesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<List<UniversityEntity>> AddUniversities(List<UniversityEntity> university)
        {
            try
            {
                await db.Universities.AddRangeAsync(university);
                await db.SaveChangesAsync();

                return university;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUniversityById(int id)
        {
            try
            {
                db.Universities.Remove(await db.Universities.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UniversityEntity>> GetAllUniversities()
        {
            try
            {
                return await db.Universities.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UniversityEntity> GetUniversityById(int id)
        {
            try
            {
                return await db.Universities.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UniversityEntity>> GetUniversitiesBySearch(string search)
        {
            try
            {
                return await db.Universities
                    .Where(uni => uni.Name.Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UniversityEntity> UpdateUniversity(UniversityEntity university)
        {
            try
            {
                db.Universities.Update(university);
                await db.SaveChangesAsync();

                return university;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
