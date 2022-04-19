using Domain;
using Entities;

namespace Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(User user)
        {
            UserEntity userEntity = new UserEntity();
            if (user != null)
            {
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.Description = user.Description;
                userEntity.Id = user.Id;
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

        public static User ToDomain(UserEntity userEntity)
        {
            User user = new User();
            if (userEntity != null)
            {
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Description = userEntity.Description;
                user.CreatedAt = userEntity.CreatedAt;
                user.Id = userEntity.Id;

                if (userEntity.TechnologyList != null)
                    user.TechnologyList = TechnologyMapper.ToDomainList(userEntity.TechnologyList);
                else
                    user.TechnologyList = new List<Technology>();

                if (userEntity.EducationList != null)
                    user.EducationList = EducationMapper.ToDomainList(userEntity.EducationList);
                else
                    user.EducationList = new List<Education>();

                if (userEntity.WorkExperienceList != null)
                    user.WorkExperienceList = WorkExperienceMapper.ToDomainList(userEntity.WorkExperienceList);
                else
                    user.WorkExperienceList = new List<WorkExperience>();

                return user;
            }
            return null;
        }

        public static List<User> ToDomainList(List<UserEntity> userEntity)
        {
            List<User> users = new List<User>();
            foreach (UserEntity user in userEntity)
            {
                users.Add(ToDomain(user));
            }
            return users;
        }

        public static List<UserEntity> ToEntityList(List<User> user)
        {
            List<UserEntity> users = new List<UserEntity>();
            foreach (User u in user)
            {
                users.Add(ToEntity(u));
            }
            return users;
        }
    }
}