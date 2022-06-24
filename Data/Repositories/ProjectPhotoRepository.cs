using Data.Entities;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public class ProjectPhotoRepository : IProjectPhotoRepository
    {
        private ApplicationContext db;
        public ProjectPhotoRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<string> DeleteProjectPhotoById(int id)
        {
            var foundProjectPhoto = await db.ProjectPhotoEntity.SingleOrDefaultAsync(x => x.Id == id);
            if (foundProjectPhoto != null)
            {
                db.ProjectPhotoEntity.Remove(foundProjectPhoto);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<ProjectPhotoEntity> GetProjectPhotoById(int id)
        {
            return await db.ProjectPhotoEntity.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}