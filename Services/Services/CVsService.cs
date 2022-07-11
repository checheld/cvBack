#region Imports
using Domain;
using Entities;
using Services.Abstract;
using AutoMapper;
using Data.Repositories.Utility.Interface;
#endregion

namespace Services
{
    public class CVsService : ICVsService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CVsService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public class AppMappingCV : Profile
        {
            public AppMappingCV()
            {
                CreateMap<CVDTO, CVEntity>().ForMember(x => x.ProjectCVList, y => y.MapFrom(t => t.ProjectCVList)).
                    ForMember(x => x.User, y => y.MapFrom(t => t.User)).ReverseMap();
                CreateMap<ProjectCVDTO, ProjectCVEntity>().ForMember(x => x.Project, y => y.MapFrom(t => t.Project)).ReverseMap();
                CreateMap<UserDTO, UserEntity>().ReverseMap();
                CreateMap<ProjectDTO, ProjectEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<CVDTO> AddCV(CVDTO cv)
        {
            try
            {
                var newModel = new CVEntity
                {
                    #region Elements
                    CVName = cv.CVName,
                    CreatedAt = DateTime.Now,
                    UserId = cv.UserId
                    #endregion
                };

                CVEntity newCV = _mapper.Map<CVEntity>(newModel);
                newCV.CreatedAt = DateTime.Now;

                var c = await _repositoryManager.CVsRepository.AddCV(newCV);

                var projectCVList = new List<ProjectCVEntity>();
                var projectCVs = cv.ProjectCVList;

                foreach (var projectCV in projectCVs)
                {
                    var newProjectCV = new ProjectCVEntity
                    {
                        #region Elements
                        Position = projectCV.Position,
                        Description = projectCV.Description,
                        StartDate = projectCV.StartDate,
                        EndDate = projectCV.EndDate,
                        ProjectId = projectCV.ProjectId,
                        CVId = c.Id
                        #endregion
                    };
                    projectCVList.Add(newProjectCV);
                }
                await _repositoryManager.CVsRepository.AddProjectCVs(projectCVList);

                c.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();
                var item = _mapper.Map<CVDTO>(c);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCVById(int id)
        {
            try
            {
                await this._repositoryManager.CVsRepository.RemoveAllProjectCVs(id);
                await _repositoryManager.CVsRepository.DeleteCVById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CVDTO> GetCVById(int id)
        {
            try
            {
                var cv = await this._repositoryManager.CVsRepository.GetCVById(id);
                cv.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();

                return _mapper.Map<CVDTO>(cv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CVDTO>> GetCVsBySearch(string search)
        {
            try
            {
                var searchCV = await _repositoryManager.CVsRepository.GetCVsBySearch(search);
                List<CVDTO> CVs = new List<CVDTO>();

                foreach (CVEntity cv in searchCV)
                {
                    cv.ProjectCVList.Select(c => { c.CV = null; c.Project.PhotoList = null; 
                        c.Project.TechnologyList = null; c.Project.CVProjectCVList = null; return c; }).ToList();
                    CVs.Add(_mapper.Map<CVDTO>(cv));
                }

                return CVs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CVDTO> UpdateCV(CVDTO cv)
        {
            try
            {
                var newModel = new CVEntity
                {
                    #region Elements
                    CVName = cv.CVName,
                    CreatedAt = cv.CreatedAt,
                    UserId = cv.UserId,
                    Id = cv.Id
                    #endregion
                };

                var c = await _repositoryManager.CVsRepository.UpdateCV(_mapper.Map<CVEntity>(newModel));

                var projectCVs = cv.ProjectCVList;
                var projectCVList = new List<ProjectCVEntity>();

                if (cv.ProjectCVList.Count() < c.ProjectCVList.Count())
                {
                    var deleteProjectCVs = c.ProjectCVList.ExceptBy(projectCVs.Select(ed => ed.Id), x => x.Id).ToList();
                    foreach (var projectCV in deleteProjectCVs)
                    {
                        await _repositoryManager.CVsRepository.DeleteProjectCV(projectCV.Id);
                    }
                }

                foreach (var projectCV in projectCVs)
                {
                    var findProjectCV = await _repositoryManager.CVsRepository.GetProjectCVById(projectCV.Id);

                    if (findProjectCV != null)
                    {
                        #region Elements
                        findProjectCV.CreatedAt = projectCV.CreatedAt;
                        findProjectCV.Position = projectCV.Position;
                        findProjectCV.Description = projectCV.Description;
                        findProjectCV.StartDate = projectCV.StartDate;
                        findProjectCV.EndDate = projectCV.EndDate;
                        findProjectCV.ProjectId = projectCV.ProjectId;
                        findProjectCV.CVId = c.Id;
                        #endregion

                        await _repositoryManager.CVsRepository.UpdateProjectCV(findProjectCV);
                    }
                    else
                    {
                        var newProjectCV = new ProjectCVEntity
                        {
                            #region Elements
                            CreatedAt = projectCV.CreatedAt,
                            Position = projectCV.Position,
                            Description = projectCV.Description,
                            StartDate = projectCV.StartDate,
                            EndDate = projectCV.EndDate,
                            ProjectId = projectCV.ProjectId,
                            CVId = c.Id
                            #endregion
                        };
                        projectCVList.Add(newProjectCV);
                    }
                }
                await _repositoryManager.CVsRepository.AddProjectCVs(projectCVList);

                c.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();

                return _mapper.Map<CVDTO>(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CVDTO>> GetAllCVs()
        {
            try
            {
                List<CVEntity> CVEntityList = await _repositoryManager.CVsRepository.GetAllCVs();

                CVEntityList.ForEach(c => c.ProjectCVList.Select(c => { c.CV = null; c.Project.TechnologyList = null; 
                    c.Project.CVProjectCVList = null; return c; }).ToList());

                List<CVDTO> CVDomainList = new List<CVDTO>();
                CVEntityList.ForEach(x => CVDomainList.Add(_mapper.Map<CVDTO>(x)));

                return CVDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}