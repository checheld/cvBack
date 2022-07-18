#region Imports
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private ApplicationContext db;
        public ProjectsRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<ProjectEntity> AddProject(ProjectEntity project)
        {
            try
            {
                await db.Projects.AddAsync(project);
                await db.SaveChangesAsync();

                return await GetProjectById(project.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddProjectTechnology(List<ProjectTechnologyEntity> projectTechnology)
        {
            try
            {
                await db.ProjectTechnology.AddRangeAsync(projectTechnology);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteProjectById(int id)
        {
            try
            {
                db.Projects.Remove(await db.Projects.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAllProjectPhotos(int projectId)
        {
            try
            {
                var get = await db.ProjectPhotoEntity.Where(x => x.ProjectId == projectId).ToListAsync();
                db.ProjectPhotoEntity.RemoveRange(get);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            try
            {
                var projects = await db.Projects
                .Include(x => x.TechnologyList)
                .Include(x => x.PhotoList)
                .ToListAsync();

                return projects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectEntity> GetProjectById(int id)
        {
            try
            {
                var project = await db.Projects
                .Include(x => x.TechnologyList).ThenInclude(z => z.ProjectList)
                .Include(x => x.PhotoList)
                .SingleOrDefaultAsync(x => x.Id == id);

                return project;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectEntity>> GetProjectsBySearch(SearchProjectsEntity searchProjects)
        {
            try
            {
                var findedTech = await db.Technologies.Where(x => x.Name == searchProjects.TechnologyName).FirstOrDefaultAsync(); 

                return await db.Projects
                    .Where(x => (!String.IsNullOrEmpty(searchProjects.Type) ? x.Type == searchProjects.Type : true) 
                    && (!String.IsNullOrEmpty(searchProjects.Name) ? x.Name.Trim().ToLower().Contains(searchProjects.Name) : true)
                    && (!String.IsNullOrEmpty(searchProjects.TechnologyName) ? x.TechnologyList.Any(z => z.Id == findedTech.Id) : true))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectEntity> UpdateProject(ProjectEntity project)
        {
            try
            {
                db.ProjectTechnology.RemoveRange(await db.ProjectTechnology.Where(x => x.ProjectId == project.Id)
                   .ToListAsync());
                await db.SaveChangesAsync();

                db.Projects.Update(project);
                await db.SaveChangesAsync();

                return await GetProjectById(project.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}