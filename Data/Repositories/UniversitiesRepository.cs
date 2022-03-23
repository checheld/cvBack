using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public class UniversitiesRepository : IUniversitiesRepository
    {
        private ApplicationContext db;
        public UniversitiesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }
        public async Task<UniversityEntity> AddUniversity(UniversityEntity university)
        {
            var foundUniversity = await db.Universities.SingleOrDefaultAsync(x => x.Id == university.Id);
            if (foundUniversity == null)
            {
                await db.Universities.AddAsync(university);
                await db.SaveChangesAsync();
                return university;
            }
            return null;
        }

        public async Task<string> DeleteUniversityById(int id)
        {
            var foundUniversity = await db.Universities.SingleOrDefaultAsync(x => x.Id == id);
            if (foundUniversity != null)
            {
                db.Universities.Remove(foundUniversity);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<UniversityEntity>> GetAllUniversities()
        {
            var universities = await db.Universities.ToListAsync();
            if (universities != null)
            {
                return universities;
            }
            return null;
        }

        public async Task<UniversityEntity> GetUniversityById(int id)
        {
            var university = await db.Universities.SingleOrDefaultAsync(x => x.Id == id);
            if (university != null)
            {
                return university;
            }
            return null;
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
