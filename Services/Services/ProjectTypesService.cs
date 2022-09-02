#region Imports
using AutoMapper;
using Data.Entities;
using Data.Repositories.Utility.Interface;
using Services.Abstract;
using Services.Domain;
#endregion

namespace Services.Services
{
    public class ProjectTypesService : IProjectTypesService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ProjectTypesService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public class AppMappingProjectType : Profile
        {
            public AppMappingProjectType()
            {
                CreateMap<ProjectTypeDTO, ProjectTypeEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<List<ProjectTypeDTO>> AddProjectTypes(List<ProjectTypeDTO> projectType)
        {
            try
            {
                var projectTypes = new List<ProjectTypeEntity>();

                foreach (var pt in projectType)
                {
                    var newProjectType = _mapper.Map<ProjectTypeEntity>(pt);
                    newProjectType.CreatedAt = DateTime.UtcNow;
                    projectTypes.Add(newProjectType);
                }

                var returnProjectTypes = await _repositoryManager.ProjectTypesRepository.AddProjectTypes(projectTypes);
                var returnProjectTypesMap = new List<ProjectTypeDTO>();

                returnProjectTypes.ForEach(c => returnProjectTypesMap.Add(_mapper.Map<ProjectTypeDTO>(c)));

                return returnProjectTypesMap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteProjectTypeById(int id)
        {
            try
            {
                await _repositoryManager.ProjectTypesRepository.DeleteProjectTypeById(id);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectTypeDTO> GetProjectTypeById(int id)
        {
            try
            {
                return _mapper.Map<ProjectTypeDTO>(await _repositoryManager.ProjectTypesRepository.GetProjectTypeById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectTypeDTO>> GetProjectTypesBySearch(string search)
        {
            try
            {
                var searchProjectTypes = await _repositoryManager.ProjectTypesRepository.GetProjectTypesBySearch(search);

                List<ProjectTypeDTO> projectTypes = new List<ProjectTypeDTO>();

                foreach (ProjectTypeEntity projectType in searchProjectTypes)
                {
                    projectTypes.Add(_mapper.Map<ProjectTypeDTO>(projectType));
                }

                return projectTypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectTypeDTO> UpdateProjectType(ProjectTypeDTO projectType)
        {
            try
            {
                return _mapper.Map<ProjectTypeDTO>(await _repositoryManager.ProjectTypesRepository
                    .UpdateProjectType(_mapper.Map<ProjectTypeEntity>(projectType)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectTypeDTO>> GetAllProjectTypes()
        {
            try
            {
                List<ProjectTypeEntity> projectTypeEntityList = await _repositoryManager.ProjectTypesRepository.GetAllProjectTypes();

                List<ProjectTypeDTO> projectTypeDomainList = new List<ProjectTypeDTO>();
                projectTypeEntityList.ForEach(x => projectTypeDomainList.Add(_mapper.Map<ProjectTypeDTO>(x)));

                return projectTypeDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}