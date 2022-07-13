#region Imports
using AutoMapper;
using Data.Entities;
using Data.Repositories.Utility.Interface;
using Domain;
using Entities;
using Services.Abstract;
#endregion

namespace Services
{
    public class UsersService : IUsersService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public UsersService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public class AppMappingUser : Profile
        {
            public AppMappingUser()
            {
                CreateMap<UserDTO, UserEntity>().ForMember(x => x.TechnologyList, y => y.MapFrom(t => t.TechnologyList)).
                    ForMember(x => x.EducationList, y => y.MapFrom(t => t.EducationList)).
                    ForMember(x => x.WorkExperienceList, y => y.MapFrom(t => t.WorkExperienceList)).ReverseMap();
                CreateMap<TechnologyDTO, TechnologyEntity>().ForMember(x => x.UserList, y => y.MapFrom(t => t.UserList)).ReverseMap();
                CreateMap<EducationDTO, EducationEntity>().ForMember(x => x.University, y => y.MapFrom(t => t.University)).ReverseMap();
                CreateMap<WorkExperienceDTO, WorkExperienceEntity>().ForMember(x => x.Company, y => y.MapFrom(t => t.Company)).ReverseMap();
                CreateMap<UniversityDTO, UniversityEntity>().ForMember(x => x.EducationUniversityList, y => y.MapFrom(t => t.EducationUniversityList)).ReverseMap();
                CreateMap<CompanyDTO, CompanyEntity>().ForMember(x => x.WorkExperienceCompanyList, y => y.MapFrom(t => t.WorkExperienceCompanyList)).ReverseMap();
                CreateMap<PhotoParamsDTO, PhotoParamsEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<UserDTO> AddUser(UserDTO user)
        {
            try
            {
                var newModel = new UserEntity
                {
                    #region Elements
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    CreatedAt = DateTime.UtcNow,
                    photoUrl = user.photoUrl,
                    PhotoParamsId = user.PhotoParamsId
                    #endregion
                };

                var newUser = _mapper.Map<UserEntity>(newModel);
                var u = await _repositoryManager.UsersRepository.AddUser(newUser);

                var technologies = user.TechnologyList;
                var links = new List<UserTechnologyEntity>();

                foreach (var technology in technologies)
                {
                    links.Add(new UserTechnologyEntity
                    {
                        UserId = u.Id,
                        TechnologyId = technology.Id
                    }
                    );
                }
                await _repositoryManager.UsersRepository.AddUserTechnology(links);

                var educations = user.EducationList;
                var educationList = new List<EducationEntity>();
                
                foreach (var education in educations)
                {
                    var newEducation = new EducationEntity
                    {
                        #region Elements
                        Speciality = education.Speciality,
                        StartDate = education.StartDate,
                        EndDate = education.EndDate,
                        CreatedAt = DateTime.UtcNow,
                        UniversityId = education.UniversityId,
                        UserId = u.Id,
                        #endregion
                    };
                    educationList.Add(newEducation);
                }
                await _repositoryManager.UsersRepository.AddEducations(educationList);

                var workExperiences = user.WorkExperienceList;
                var workExperienceList = new List<WorkExperienceEntity>();

                foreach (var workExperience in workExperiences)
                {
                    var newWorkExperience = new WorkExperienceEntity
                    {
                        #region Elements
                        Position = workExperience.Position,
                        StartDate = workExperience.StartDate,
                        EndDate = workExperience.EndDate,
                        Description = workExperience.Description,
                        CreatedAt = DateTime.UtcNow,
                        CompanyId = workExperience.CompanyId,
                        UserId = u.Id
                        #endregion
                    };
                    workExperienceList.Add(newWorkExperience);
                }
                await _repositoryManager.UsersRepository.AddWorkExperiences(workExperienceList);
                #region Remove Looping
                u.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                u.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                u.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                #endregion
                var item = _mapper.Map<UserDTO>(u);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUserById(int id)
        {
            try
            {
                var getUser = await this._repositoryManager.UsersRepository.GetUserById(id);
              
                await this._repositoryManager.UsersRepository.RemoveAllEducations(getUser.Id);
                await this._repositoryManager.UsersRepository.RemoveAllWorkExperiences(getUser.Id);
                await _repositoryManager.UsersRepository.DeleteUserById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            try
            {
                var user = await _repositoryManager.UsersRepository.GetUserById(id);
                #region Remove Looping
                user.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                user.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                user.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                #endregion
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<UserDTO>> GetUsersBySearch(string search)
        {
            try
            {
                var searchUsersRepo = await _repositoryManager.UsersRepository.GetUsersBySearch(search);

                List<UserDTO> users = new List<UserDTO>();

                foreach (UserEntity user in searchUsersRepo)
                {
                    #region Remove Looping
                    user.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                    user.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                    user.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                    #endregion
                    users.Add(_mapper.Map<UserDTO>(user));
                }

                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            try
            {
                var newModel = new UserEntity
                {
                    #region Elements
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    photoUrl = user.photoUrl,
                    Id = user.Id,
                    PhotoParamsId = user.PhotoParamsId
                    #endregion
                };

                var u = await _repositoryManager.UsersRepository.UpdateUser(_mapper.Map<UserEntity>(newModel));

                var technologies = user.TechnologyList;
                var links = new List<UserTechnologyEntity>();

                foreach (var technology in user.TechnologyList)
                {
                    links.Add(new UserTechnologyEntity
                        {
                            UserId = u.Id,
                            TechnologyId = technology.Id
                        }
                    );
                }
                await _repositoryManager.UsersRepository.AddUserTechnology(links);

                var educations = user.EducationList;
                var educationList = new List<EducationEntity>();

                if (user.EducationList.Count() < u.EducationList.Count())
                {
                    var deleteEducations = u.EducationList.ExceptBy(educations.Select(ed => ed.Id), x => x.Id).ToList();
                    foreach (var education in deleteEducations)
                    {
                        await _repositoryManager.UsersRepository.DeleteEducation(education.Id);
                    }
                }

                foreach (var education in educations)
                {
                    var findEducation = await _repositoryManager.UsersRepository.GetEducationById(education.Id);

                    if (findEducation != null)
                    {
                        #region Elements
                        findEducation.Speciality = education.Speciality;
                        findEducation.StartDate = education.StartDate;
                        findEducation.EndDate = education.EndDate;
                        findEducation.UniversityId = education.UniversityId;
                        findEducation.UserId = u.Id;
                        findEducation.CreatedAt = education.CreatedAt;
                        #endregion
                        await _repositoryManager.UsersRepository.UpdateEducation(findEducation);
                    }
                    else
                    {
                        var newEducation = new EducationEntity
                        {
                            #region Elements
                            Speciality = education.Speciality,
                            StartDate = education.StartDate,
                            EndDate = education.EndDate,
                            UniversityId = education.UniversityId,
                            UserId = u.Id,
                            CreatedAt = education.CreatedAt
                            #endregion
                        };
                        educationList.Add(newEducation);
                    }
                }
                await _repositoryManager.UsersRepository.AddEducations(educationList);

                var workExperiences = user.WorkExperienceList;
                var workExperienceList = new List<WorkExperienceEntity>();

                if (user.WorkExperienceList.Count() < u.WorkExperienceList.Count())
                {
                    var deleteWorkExperiences = u.WorkExperienceList.ExceptBy(workExperiences.Select(ed => ed.Id), x => x.Id).ToList();

                    foreach (var workExp in deleteWorkExperiences)
                    {
                        await _repositoryManager.UsersRepository.DeleteWorkExperience(workExp.Id);
                    }
                }

                foreach (var workExperience in workExperiences)
                {
                    var findWorkExperience = await _repositoryManager.UsersRepository.GetWorkExperienceById(workExperience.Id);

                    if (findWorkExperience != null)
                    {
                        #region Elements
                        findWorkExperience.Position = workExperience.Position;
                        findWorkExperience.Description = workExperience.Description;
                        findWorkExperience.StartDate = workExperience.StartDate;
                        findWorkExperience.EndDate = workExperience.EndDate;
                        findWorkExperience.CompanyId = workExperience.CompanyId;
                        findWorkExperience.UserId = u.Id;
                        findWorkExperience.CreatedAt = workExperience.CreatedAt;
                        #endregion
                        await _repositoryManager.UsersRepository.UpdateWorkExperience(findWorkExperience);
                    }
                    else
                    {
                        var newWorkExperience = new WorkExperienceEntity
                        {
                            #region Elements
                            Position = workExperience.Position,
                            Description = workExperience.Description,
                            StartDate = workExperience.StartDate,
                            EndDate = workExperience.EndDate,
                            CompanyId = workExperience.CompanyId,
                            UserId = u.Id,
                            CreatedAt = workExperience.CreatedAt
                            #endregion
                        };
                        workExperienceList.Add(newWorkExperience);
                    }
                }
                await _repositoryManager.UsersRepository.AddWorkExperiences(workExperienceList);
                #region Remove Looping
                u.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                u.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                u.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                #endregion
                return _mapper.Map<UserDTO>(u);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                List<UserEntity> userEntityList = await _repositoryManager.UsersRepository.GetAllUsers();
                #region Remove Looping
                userEntityList.ForEach(c=> c.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList());
                userEntityList.ForEach(c => c.EducationList.Select(c => { c.University = null; c.User = null; return c; }).ToList());
                userEntityList.ForEach(c => c.WorkExperienceList.Select(c => { c.Company = null; c.User = null; return c; }).ToList());
                #endregion
                List<UserDTO> userDomainList = new List<UserDTO>();

                userEntityList.ForEach(x => userDomainList.Add(_mapper.Map<UserDTO>(x)));

                return userDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}