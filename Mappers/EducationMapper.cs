using Domain;
using Entities;

namespace Mappers
{
    public static class EducationMapper
    {
        public static EducationEntity ToEntity(EducationDTO education)
        {
            EducationEntity educationEntity = new EducationEntity();
            if (education != null)
            {
                educationEntity.University = UniversityMapper.ToEntity(education.University);
                educationEntity.Speciality = education.Speciality;
                educationEntity.UniversityId = education.UniversityId;
                educationEntity.UserId = education.UserId;

                educationEntity.StartDate = education.StartDate;
                educationEntity.EndDate = education.EndDate;
                educationEntity.Id = education.Id;
                educationEntity.CreatedAt = DateTime.Now;
                
                return educationEntity;
            }
            return null;
        }

        public static EducationDTO ToDomain(EducationEntity educationEntity)
        {
            EducationDTO education = new EducationDTO();
            if (educationEntity != null)
            {
                education.University = UniversityMapper.ToDomain(educationEntity.University);
                education.UniversityId = educationEntity.UniversityId;
                education.Speciality = educationEntity.Speciality;
                education.StartDate = educationEntity.StartDate;
                education.EndDate = educationEntity.EndDate;
                education.Id = educationEntity.Id;
                education.CreatedAt = educationEntity.CreatedAt;

                return education;
            }
            return null;
        }

        public static List<EducationDTO> ToDomainList(List<EducationEntity> educationEntity)
        {
            List<EducationDTO> educations = new List<EducationDTO>();
            foreach (EducationEntity education in educationEntity)
            {
                educations.Add(ToDomain(education));
            }
            return educations;
        }

        public static List<EducationEntity> ToEntityList(List<EducationDTO> education)
        {
            List<EducationEntity> educations = new List<EducationEntity>();
            foreach (EducationDTO e in education)
            {
                educations.Add(ToEntity(e));
            }
            return educations;
        }
    }
}