using Domain;
using Entities;

namespace Mappers
{
    public static class UniversityMapper
    {
        public static UniversityEntity ToEntity(University university)
        {
            UniversityEntity universityEntity = new UniversityEntity();
           
                if(university != null)
                {
                    universityEntity.Name = university.Name;
                    universityEntity.Id = university.Id;
                    universityEntity.CreatedAt = DateTime.Now;
  
                return universityEntity;
                }
                return null;
        }

        public static University ToDomain(UniversityEntity universityEntity)
        {
            University university = new University();
            if (universityEntity != null) 
            {
                university.Name = universityEntity.Name;
                university.CreatedAt = universityEntity.CreatedAt;
                university.Id = universityEntity.Id;

                return university;
            }
            return null;
        }
        public static List<University> ToDomainList(List<UniversityEntity> universityEntity)
        {
            List<University> universities = new List<University>();
            foreach (UniversityEntity univer in universityEntity)
            {
                universities.Add(ToDomain(univer));
            }
            return universities;
        }
    }
}
