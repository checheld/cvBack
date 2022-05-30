using Domain;
using Entities;

namespace Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(UserDTO user)
        {
            UserEntity userEntity = new UserEntity();
            if (user != null)
            {
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.Description = user.Description;
                userEntity.Id = user.Id;
                userEntity.photoUrl = user.photoUrl;
                userEntity.CreatedAt = DateTime.Now;

                if (user.TechnologyList != null)
                    userEntity.TechnologyList = TechnologyMapper.ToEntityList(user.TechnologyList);
                else
                    userEntity.TechnologyList = new List<TechnologyEntity>();

                if (user.EducationList != null)
                    userEntity.EducationList = EducationMapper.ToEntityList(user.EducationList);
                else
                    userEntity.EducationList = new List<EducationEntity>();

                if (user.WorkExperienceList != null)
                    userEntity.WorkExperienceList = WorkExperienceMapper.ToEntityList(user.WorkExperienceList);
                else
                    userEntity.WorkExperienceList = new List<WorkExperienceEntity>();

                return userEntity;
            }
            return null;
        }

        public static UserDTO ToDomain(UserEntity userEntity)
        {
            UserDTO user = new UserDTO();
            if (userEntity != null)
            {
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Description = userEntity.Description;
                user.CreatedAt = userEntity.CreatedAt;
                user.Id = userEntity.Id;
                user.photoUrl = userEntity.photoUrl;

                if (userEntity.TechnologyList != null)
                    user.TechnologyList = TechnologyMapper.ToDomainList(userEntity.TechnologyList);
                else
                    user.TechnologyList = new List<TechnologyDTO>();

                if (userEntity.EducationList != null)
                    user.EducationList = EducationMapper.ToDomainList(userEntity.EducationList);
                else
                    user.EducationList = new List<EducationDTO>();

                if (userEntity.WorkExperienceList != null)
                    user.WorkExperienceList = WorkExperienceMapper.ToDomainList(userEntity.WorkExperienceList);
                else
                    user.WorkExperienceList = new List<WorkExperienceDTO>();

                return user;
            }
            return null;
        }

        public static List<UserDTO> ToDomainList(List<UserEntity> userEntity)
        {
            List<UserDTO> users = new List<UserDTO>();
            foreach (UserEntity user in userEntity)
            {
                users.Add(ToDomain(user));
            }
            return users;
        }

        public static List<UserEntity> ToEntityList(List<UserDTO> user)
        {
            List<UserEntity> users = new List<UserEntity>();
            foreach (UserDTO u in user)
            {
                users.Add(ToEntity(u));
            }
            return users;
        }
    }
}