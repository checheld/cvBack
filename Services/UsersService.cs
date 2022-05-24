using AutoMapper;
using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class UsersService : IUsersService
    {

        private readonly IUsersRepository _usersRepository;
        /*private readonly IMapper _mapper;*/

        public UsersService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            /*_mapper = mapper;*/
            _usersRepository = _serviceProvider.GetService<IUsersRepository>();
        }

        // autoMapper
        /*public class AppMappingProfile : Profile
        {
            public AppMappingProfile()
            {
                CreateMap<UserDTO, UserEntity>().ReverseMap();
            }
        }*/
        //

        public async Task<UserDTO> AddUser(UserDTO user)
        {
            try
            {
                UserEntity newUser = UserMapper.ToEntity(user);
                /*var newUser = _mapper.Map<UserEntity>(user);*/
                UserEntity u = await _usersRepository.AddUser(newUser);
                if (u != null)
                {
                    UserDTO item = UserMapper.ToDomain(u);
                    /*var item = _mapper.Map<UserDTO>(u);*/
                    return item;
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
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
                return UserMapper.ToDomain(await _usersRepository.GetUserById(id));
                /*return _mapper.Map<UserDTO>(await _usersRepository.GetUserById(id));*/
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
                return UserMapper.ToDomainList(await _usersRepository.GetUsersBySearch(search));

                /*var searchUsers = await _usersRepository.GetUsersBySearch(search);
                List<UserDTO> users = new List<UserDTO>();
                foreach (UserEntity user in searchUsers)
                {
                    _mapper.Map<UserEntity>(user);
                }
                return users;*/
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
                var getUser = await this._usersRepository.GetUserById(user.Id);
              
                await this._usersRepository.RemoveAllEducations(getUser.Id);
                await this._usersRepository.RemoveAllWorkExperiences(getUser.Id);

                return UserMapper.ToDomain(await _usersRepository.UpdateUser(UserMapper.ToEntity(user)));
                /*return _mapper.Map<UserDTO>(await _usersRepository.UpdateUser(UserMapper.ToEntity(user)));*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        // here
        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                List<UserEntity> userEntityList = await _usersRepository.GetAllUsers();
                List<UserDTO> userDomainList = new List<UserDTO>();
                userEntityList.ForEach(x => userDomainList.Add(UserMapper.ToDomain(x)));
                /*userEntityList.ForEach(x => userDomainList.Add(_mapper.Map<UserDTO>(x)));*/
               
                return userDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}