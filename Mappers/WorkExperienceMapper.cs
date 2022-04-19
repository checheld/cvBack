using Domain;
using Entities;

namespace Mappers
{
    public static class WorkExperienceMapper
    {
        public static WorkExperienceEntity ToEntity(WorkExperience workExperience)
        {
            WorkExperienceEntity workExperienceEntity = new WorkExperienceEntity();
            if (workExperience != null)
            {
                workExperienceEntity.CompanyId = workExperience.CompanyId;
                workExperienceEntity.Company = CompanyMapper.ToEntity(workExperience.Company);
                workExperienceEntity.Position = workExperience.Position;
                workExperienceEntity.StartDate = workExperience.StartDate;
                workExperienceEntity.EndDate = workExperience.EndDate;
                workExperienceEntity.Description = workExperience.Description;
                workExperienceEntity.Id = workExperience.Id;
                workExperienceEntity.CreatedAt = DateTime.Now;
                workExperienceEntity.UserId = workExperience.UserId;
                return workExperienceEntity;
            }
            return null;
        }

        public static WorkExperience ToDomain(WorkExperienceEntity workExperienceEntity)
        {
            WorkExperience workExperience = new WorkExperience();
            if (workExperienceEntity != null)
            {
                workExperience.Company = CompanyMapper.ToDomain(workExperienceEntity.Company);
                workExperience.Position = workExperienceEntity.Position;
                workExperience.StartDate = workExperienceEntity.StartDate;
                workExperience.EndDate = workExperienceEntity.EndDate;
                workExperience.Description = workExperienceEntity.Description;
                workExperience.Id = workExperienceEntity.Id;
                workExperience.CreatedAt = workExperienceEntity.CreatedAt;

                return workExperience;
            }
            return null;
        }

        public static List<WorkExperience> ToDomainList(List<WorkExperienceEntity> workExperienceEntity)
        {
            List<WorkExperience> workExperiences = new List<WorkExperience>();
            foreach (WorkExperienceEntity workExperience in workExperienceEntity)
            {
                workExperiences.Add(ToDomain(workExperience));
            }
            return workExperiences;
        }

        public static List<WorkExperienceEntity> ToEntityList(List<WorkExperience> workExperience)
        {
            List<WorkExperienceEntity> workExperiences = new List<WorkExperienceEntity>();
            foreach (WorkExperience w in workExperience)
            {
                workExperiences.Add(ToEntity(w));
            }
            return workExperiences;
        }
    }
}