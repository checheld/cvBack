#region Imports
using Data.Entities;
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class ProjectTypesRepository : IProjectTypesRepository
    {
        private ApplicationContext db;
        public ProjectTypesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<List<ProjectTypeEntity>> AddProjectTypes(List<ProjectTypeEntity> projectType)
        {
            try
            {
                await db.ProjectTypes.AddRangeAsync(projectType);
                await db.SaveChangesAsync();

                return projectType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteProjectTypeById(int id)
        {
            try
            {
                db.ProjectTypes.Remove(await db.ProjectTypes.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectTypeEntity>> GetAllProjectTypes()
        {
            try
            {
                return await db.ProjectTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectTypeEntity> GetProjectTypeById(int id)
        {
            try
            {
                return await db.ProjectTypes.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectTypeEntity>> GetProjectTypesBySearch(string search)
        {
            try
            {
                return await db.ProjectTypes
                    .Where(pt => pt.Name.Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectTypeEntity> UpdateProjectType(ProjectTypeEntity projectType)
        {
            try
            {
                db.ProjectTypes.Update(projectType);
                await db.SaveChangesAsync();

                return projectType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}