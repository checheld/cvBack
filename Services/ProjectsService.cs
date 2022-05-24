using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using AutoMapper;

namespace Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IMapper _mapper;
        public ProjectsService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            _mapper = mapper;
            _projectsRepository = _serviceProvider.GetService<IProjectsRepository>();
        }

        // autoMapper
        public class AppMappingProject : Profile
        {
            public AppMappingProject()
            {
                CreateMap<ProjectDTO, ProjectEntity>().ReverseMap();
                CreateMap<TechnologyDTO, TechnologyEntity>().ReverseMap();
            }
        }
        //
        
        public async Task<ProjectDTO> AddProject(ProjectDTO project)
        {
            try
            {
                /*var newProject = ProjectMapper.ToEntity(project);*/
                var newProject = _mapper.Map<ProjectEntity>(project);
                newProject.CreatedAt = DateTime.Now;
                //
                List<TechnologyEntity> technologies = new List<TechnologyEntity>();
                if (newProject.TechnologyList != null)
                    
                foreach (TechnologyEntity tech in newProject.TechnologyList)
                {
                    technologies.Add(_mapper.Map<TechnologyEntity>(tech));
                }
                else
                    technologies = new List<TechnologyEntity>();
                newProject.TechnologyList = technologies;
                //
                var c = await _projectsRepository.AddProject(newProject);
                c.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();

                /*return ProjectMapper.ToDomain(c);*/
                var item = _mapper.Map<ProjectDTO>(c);
                return item;
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
        
        public async Task<ProjectDTO> GetProjectById(int id)
        {
            try
            {
                var c = await _projectsRepository.GetProjectById(id);
                c.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();
                /*return ProjectMapper.ToDomain(await _projectsRepository.GetProjectById(id));*/
                return _mapper.Map<ProjectDTO>(await _projectsRepository.GetProjectById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<List<ProjectDTO>> GetProjectsBySearch(SearchProjectsDTO searchProjects)
        {
            try
            {
                var searchProjectToEntity = new SearchProjectsEntity()
                {
                    Type = searchProjects.Type,
                    Name = searchProjects.Name,
                    TechnologyName = searchProjects.TechnologyName
                };
                /*return ProjectMapper.ToDomainList(await _projectsRepository.GetProjectsBySearch(searchProjectToEntity));*/

                var searchProjectsRepo = await _projectsRepository.GetProjectsBySearch(searchProjectToEntity);
                List<ProjectDTO> projects = new List<ProjectDTO>();
                foreach (ProjectEntity project in searchProjectsRepo)
                {
                    projects.Add(_mapper.Map<ProjectDTO>(project));
                }
                return projects;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ProjectDTO> UpdateProject(ProjectDTO project)
        {
            try
            {
                /*return ProjectMapper.ToDomain(await _projectsRepository.UpdateProject(ProjectMapper.ToEntity(project)));*/
                return _mapper.Map<ProjectDTO>(await _projectsRepository.UpdateProject(_mapper.Map<ProjectEntity>(project)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            try
            {
                List<ProjectEntity> projectEntityList = await _projectsRepository.GetAllProjects();
                projectEntityList.ForEach(x => x.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList());
                List<ProjectDTO> projectDomainList = new List<ProjectDTO>();
                /*projectEntityList.ForEach(x => projectDomainList.Add(ProjectMapper.ToDomain(x)));*/
                projectEntityList.ForEach(x => projectDomainList.Add(_mapper.Map<ProjectDTO>(x)));
                return projectDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}