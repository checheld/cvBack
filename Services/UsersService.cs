using AutoMapper;
using Data.Repositories.Abstract;
using Domain;
using Entities;
/*using Mappers;*/
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            _mapper = mapper;
            _usersRepository = _serviceProvider.GetService<IUsersRepository>();
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
            }
        }

        public async Task<UserDTO> AddUser(UserDTO user)
        {
            try
            {
                /*UserEntity newUser = UserMapper.ToEntity(user);*/
                var newUser = _mapper.Map<UserEntity>(user);
                newUser.CreatedAt = DateTime.Now;

                var u = await _usersRepository.AddUser(newUser);
                u.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                u.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                u.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                /*UserDTO item = UserMapper.ToDomain(u);*/
                var item = _mapper.Map<UserDTO>(u);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteUserById(int id)
        {
            try
            {
                var getUser = await this._usersRepository.GetUserById(id);
              
                await this._usersRepository.RemoveAllEducations(getUser.Id);
                await this._usersRepository.RemoveAllWorkExperiences(getUser.Id);

                return await _usersRepository.DeleteUserById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            try
            {
                /*return UserMapper.ToDomain(await _usersRepository.GetUserById(id));*/
                var user = await _usersRepository.GetUserById(id);
                user.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                user.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                user.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<List<UserDTO>> GetUsersBySearch(string search)
        {
            try
            {
                var searchUsersRepo = await _usersRepository.GetUsersBySearch(search);
                List<UserDTO> users = new List<UserDTO>();
                foreach (UserEntity user in searchUsersRepo)
                {
                    user.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                    user.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                    user.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                    users.Add(_mapper.Map<UserDTO>(user));
                }
                return users;
                /*return UserMapper.ToDomainList(await _usersRepository.GetUsersBySearch(search));*/
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            try
            {
                var u = await _usersRepository.UpdateUser(_mapper.Map<UserEntity>(user));
                u.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList();
                u.EducationList.Select(c => { c.University.EducationUniversityList = null; c.User = null; return c; }).ToList();
                u.WorkExperienceList.Select(c => { c.Company.WorkExperienceCompanyList = null; c.User = null; return c; }).ToList();
                return _mapper.Map<UserDTO>(u);

                /*var getUser = await this._usersRepository.GetUserById(user.Id);
                return UserMapper.ToDomain(await _usersRepository.UpdateUser(UserMapper.ToEntity(user)));*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                List<UserEntity> userEntityList = await _usersRepository.GetAllUsers();
                userEntityList.ForEach(c=> c.TechnologyList.Select(c => { c.ProjectList = null; c.UserList = null; return c; }).ToList());
                userEntityList.ForEach(c => c.EducationList.Select(c => { c.University = null; c.User = null; return c; }).ToList());
                userEntityList.ForEach(c => c.WorkExperienceList.Select(c => { c.Company = null; c.User = null; return c; }).ToList());
                List<UserDTO> userDomainList = new List<UserDTO>();
                userEntityList.ForEach(x => userDomainList.Add(_mapper.Map<UserDTO>(x)));
                /*userEntityList.ForEach(x => userDomainList.Add(UserMapper.ToDomain(x)));*/

                return userDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}