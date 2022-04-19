using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        public ProjectsService(IServiceProvider _serviceProvider)
        {
            _projectsRepository = _serviceProvider.GetService<IProjectsRepository>();
        }

        public async Task<Project> AddProject(Project project)
        {
            try
            {
                var newProject = ProjectMapper.ToEntity(project);
                var c = await _projectsRepository.AddProject(newProject);
                c.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();

                return ProjectMapper.ToDomain(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteProjectById(int id)
        {
            try
            {
                return await _projectsRepository.DeleteProjectById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Project> GetProjectById(int id)
        {
            try
            {
                var c = await _projectsRepository.GetProjectById(id);
                c.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();
                return ProjectMapper.ToDomain(await _projectsRepository.GetProjectById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Project>> GetProjectsBySearch(SearchProjects searchProjects)
        {
            try
            {
                var searchProjectToEntity = new SearchProjectsEntity()
                {
                    Type = searchProjects.Type,
                    Name = searchProjects.Name,
                    TechnologyName = searchProjects.TechnologyName
                };
                return ProjectMapper.ToDomainList(await _projectsRepository.GetProjectsBySearch(searchProjectToEntity));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Project> UpdateProject(Project project)
        {
            try
            {
                return ProjectMapper.ToDomain(await _projectsRepository.UpdateProject(ProjectMapper.ToEntity(project)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Project>> GetAllProjects()
        {
            try
            {
                List<ProjectEntity> projectEntityList = await _projectsRepository.GetAllProjects();
                projectEntityList.ForEach(x => x.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList());
                List<Project> projectDomainList = new List<Project>();
                projectEntityList.ForEach(x => projectDomainList.Add(ProjectMapper.ToDomain(x)));
                return projectDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}