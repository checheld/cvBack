#region Imports
using Domain;
using Entities;
using Services.Abstract;
using AutoMapper;
using Data.Entities;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Data.Repositories.Utility.Interface;
#endregion

namespace Services
{
    public class ProjectsService : IProjectsService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        private Account CloudinaryAccount { get; }
        private Cloudinary _cloudinary;

        public ProjectsService(IMapper mapper, IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            Configuration = configuration;

            CloudinaryAccount = new Account(Configuration.GetSection("CloudinarySettings")["CloudName"],
                 Configuration.GetSection("CloudinarySettings")["ApiKey"],
                 Configuration.GetSection("CloudinarySettings")["ApiSecret"]);
        }

        public class AppMappingProject : Profile
        {
            public AppMappingProject()
            {
                CreateMap<ProjectDTO, ProjectEntity>().ForMember(x=>x.TechnologyList, y=>y.MapFrom(t=>t.TechnologyList)).
                    ForMember(x => x.PhotoList, y => y.MapFrom(t => t.PhotoList)).ReverseMap();
                CreateMap<TechnologyDTO, TechnologyEntity>().ForMember(x => x.ProjectList, y => y.MapFrom(t => t.ProjectList)).ReverseMap();
                CreateMap<ProjectPhotoDTO, ProjectPhotoEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<ProjectDTO> AddProject(ProjectDTO project)
        {
            try
            {
                var newModel = new ProjectDTO
                {
                    #region Elements
                    Country = project.Country,
                    CreatedAt = DateTime.UtcNow,
                    Description = project.Description,
                    Link = project.Link,
                    Name = project.Name,
                    ProjectTypeId = project.ProjectTypeId,
                    #endregion
                };

                var newProject = _mapper.Map<ProjectEntity>(newModel);
                var c = await _repositoryManager.ProjectsRepository.AddProject(newProject);

                var technologies = project.TechnologyList;
                var links = new List<ProjectTechnologyEntity>();
               
                foreach (var technology in technologies)
                {
                    links.Add(new ProjectTechnologyEntity
                        {
                            ProjectId = c.Id,
                            TechnologyId = technology.Id
                        }
                    );
                }
                await _repositoryManager.ProjectsRepository.AddProjectTechnology(links);

                var projectPhotos = project.PhotoList;
                var projectPhotoList = new List<ProjectPhotoEntity>();

                foreach (var projectPhoto in projectPhotos)
                {
                    var newProjectPhoto = new ProjectPhotoEntity
                    {
                        #region Elements
                        Url = projectPhoto.Url,
                        CreatedAt = DateTime.UtcNow,
                        ProjectId = c.Id,
                        #endregion
                    };
                    projectPhotoList.Add(newProjectPhoto);
                }
                await _repositoryManager.ProjectPhotoRepository.AddProjectPhotos(projectPhotoList);

                c.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();
                c.PhotoList.Select(c => { c.Project = null; return c; }).ToList();
                c.ProjectType.ProjectProjectTypeList.Select(c => { c.ProjectType = null; return c; }).ToList();

                var item = _mapper.Map<ProjectDTO>(c);

                return item;
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
               var getProject = await _repositoryManager.ProjectsRepository.GetProjectById(id);

                var urlList = new List<string>();
                getProject.PhotoList.ForEach(x => urlList.Add(x.Url));

                urlList.ForEach(x => 
                {
                    var prodId1 = x.Split("upload/")[1];
                    var prodId2 = prodId1.Split("/")[1];
                    var prodId3 = prodId2.Split(".")[0];
                    new Cloudinary(CloudinaryAccount).DeleteResourcesAsync(prodId3);
                });

                await this._repositoryManager.ProjectsRepository.RemoveAllProjectPhotos(getProject.Id);
                await _repositoryManager.ProjectsRepository.DeleteProjectById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ProjectDTO> GetProjectById(int id)
        {
            try
            {
                var c = await _repositoryManager.ProjectsRepository.GetProjectById(id);
                c.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList();
                c.PhotoList.Select(c => { c.Project = null; return c; }).ToList();
                c.ProjectType.ProjectProjectTypeList = null;

                return _mapper.Map<ProjectDTO>(await _repositoryManager.ProjectsRepository.GetProjectById(id));
            }
            catch (Exception ex)
            {
                throw ex;
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

                var searchProjectsRepo = await _repositoryManager.ProjectsRepository.GetProjectsBySearch(searchProjectToEntity);

                List<ProjectDTO> projects = new List<ProjectDTO>();

                foreach (ProjectEntity project in searchProjectsRepo)
                {
                    projects.Add(_mapper.Map<ProjectDTO>(project));
                }

                return projects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectDTO> UpdateProject(ProjectDTO project)
        {
            try
            {
                var newModel = new ProjectEntity
                {
                    #region Elements
                    Id = project.Id,
                    Country = project.Country,
                    CreatedAt = project.CreatedAt,
                    Description = project.Description,
                    Link = project.Link,
                    Name = project.Name,
                    ProjectTypeId = project.ProjectTypeId,
                    #endregion
                };

                var p = await _repositoryManager.ProjectsRepository.UpdateProject(_mapper.Map<ProjectEntity>(newModel));
                p.PhotoList.Select(c => { c.Project = null; return c; }).ToList();

                var links = new List<ProjectTechnologyEntity>();
                var technologies = project.TechnologyList;

                foreach (var technology in technologies)
                {
                    links.Add(new ProjectTechnologyEntity
                    {
                        ProjectId = project.Id,
                        TechnologyId = technology.Id
                    }
                    );
                }
                await _repositoryManager.ProjectsRepository.AddProjectTechnology(links);

                var projectPhotos = project.PhotoList;
                var PhotoList = new List<ProjectPhotoEntity>();

                if (project.PhotoList.Count() < p.PhotoList.Count())
                {
                    var deleteProjectPhotos = p.PhotoList.ExceptBy(projectPhotos.Select(ed => ed.Id), x => x.Id).ToList();
                    foreach (var projectPhoto in deleteProjectPhotos)
                    {
                        await _repositoryManager.ProjectPhotoRepository.DeleteProjectPhotoById(projectPhoto.Id);
                    }
                }

                foreach (var projectPhoto in projectPhotos)
                {
                    var findProjectPhoto = await _repositoryManager.ProjectPhotoRepository.GetProjectPhotoById(projectPhoto.Id);

                    if (findProjectPhoto != null)
                    {
                        #region Elements
                        findProjectPhoto.Url = projectPhoto.Url;
                        findProjectPhoto.ProjectId = p.Id;
                        findProjectPhoto.CreatedAt = projectPhoto.CreatedAt;
                        #endregion
                        await _repositoryManager.ProjectPhotoRepository.UpdateProjectPhoto(findProjectPhoto);
                    }
                    else
                    {
                        var newProjectPhoto = new ProjectPhotoEntity
                        {
                            #region Elements
                            Url = projectPhoto.Url,
                            ProjectId = p.Id,
                            CreatedAt = DateTime.UtcNow
                            #endregion
                        };
                        PhotoList.Add(newProjectPhoto);
                    }
                }
                await _repositoryManager.ProjectPhotoRepository.AddProjectPhotos(PhotoList);

                return _mapper.Map<ProjectDTO>(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            try
            {
                List<ProjectEntity> projectEntityList = await _repositoryManager.ProjectsRepository.GetAllProjects();

                projectEntityList.ForEach(x => x.TechnologyList.Select(c => { c.ProjectList = null; return c; }).ToList());
                projectEntityList.ForEach(x => x.PhotoList.Select(c => { c.Project = null; return c; }).ToList());
                projectEntityList.ForEach(x => x.ProjectType.ProjectProjectTypeList = null);
                List <ProjectDTO> projectDomainList = new List<ProjectDTO>();
                projectEntityList.ForEach(x => projectDomainList.Add(_mapper.Map<ProjectDTO>(x)));

                return projectDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}