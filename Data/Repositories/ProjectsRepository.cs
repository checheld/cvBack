using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                var newModel = new ProjectEntity
                {
                    Country = project.Country,
                    CreatedAt = project.CreatedAt,
                    Description = project.Description,
                    Link = project.Link,
                    Name = project.Name,
                    Type = project.Type,
                };
                var technologies = project.TechnologyList;
                var links = new List<ProjectTechnology>();  

                await db.Projects.AddAsync(newModel);
                await db.SaveChangesAsync();

                var addedProject = await db.Projects.Where(x => x.CreatedAt == newModel.CreatedAt)
                    .FirstOrDefaultAsync();
                foreach (var technology in technologies)
                {
                    links.Add(new ProjectTechnology
                        {
                            ProjectId = addedProject.Id,
                            TechnologyId = technology.Id
                        }
                    );
                }
                await db.ProjectTechnology.AddRangeAsync(links);

                await db.SaveChangesAsync();

                project.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();

                return await GetProjectById(newModel.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteProjectById(int id)
        {
            var foundProject = await db.Projects.SingleOrDefaultAsync(x => x.Id == id);
            if (foundProject != null)
            {
                db.Projects.Remove(foundProject);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            var projects = await db.Projects.Include(x => x.TechnologyList).ToListAsync();

            if (projects != null)
            {
                return projects;
            }
            return null;
        }

        public async Task<ProjectEntity> GetProjectById(int id)
        {
            var project = await db.Projects.Include(x => x.TechnologyList).ThenInclude(z => z.ProjectList).SingleOrDefaultAsync(x => x.Id == id);

            if (project != null)
            {
                return project;
            }
            return null;
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
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public async Task<ProjectEntity> UpdateProject(ProjectEntity project)
        {
            try
            {
                var links = new List<ProjectTechnology>();
                var findededConnection = await db.ProjectTechnology.Where(x => x.ProjectId == project.Id)
                   .ToListAsync();
                db.ProjectTechnology.RemoveRange(findededConnection);
                await db.SaveChangesAsync();

                var technologies = project.TechnologyList;
                foreach (var technology in technologies)
                {
                    links.Add(new ProjectTechnology
                    {
                        ProjectId = project.Id,
                        TechnologyId = technology.Id
                    }
                    );
                }
                await db.ProjectTechnology.AddRangeAsync(links);
                db.Projects.Update(project);
                await db.SaveChangesAsync();
                project.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();
                return project;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}