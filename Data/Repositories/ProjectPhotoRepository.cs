#region Imports
using Data.Entities;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class ProjectPhotoRepository : IProjectPhotoRepository
    {
        private ApplicationContext db;
        public ProjectPhotoRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task DeleteProjectPhotoById(int id)
        {
            try
            {
                db.ProjectPhotoEntity.Remove(await db.ProjectPhotoEntity.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectPhotoEntity> GetProjectPhotoById(int id)
        {
            try
            {
                return await db.ProjectPhotoEntity.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectPhotoEntity> UpdateProjectPhoto(ProjectPhotoEntity projectPhoto)
        {
            try
            {
                db.ProjectPhotoEntity.Update(projectPhoto);
                await db.SaveChangesAsync();

                return projectPhoto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddProjectPhotos(List<ProjectPhotoEntity> projectPhoto)
        {
            try
            {
                await db.ProjectPhotoEntity.AddRangeAsync(projectPhoto);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}