using Domain;
using Entities;

namespace Mappers
{
    public static class UniversityMapper
    {
        public static UniversityEntity ToEntity(UniversityDTO university)
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

        public static UniversityDTO ToDomain(UniversityEntity universityEntity)
        {
            UniversityDTO university = new UniversityDTO();
            if (universityEntity != null) 
            {
                university.Name = universityEntity.Name;
                university.CreatedAt = universityEntity.CreatedAt;
                university.Id = universityEntity.Id;

                return university;
            }
            return null;
        }
        public static List<UniversityDTO> ToDomainList(List<UniversityEntity> universityEntity)
        {
            List<UniversityDTO> universities = new List<UniversityDTO>();
            foreach (UniversityEntity univer in universityEntity)
            {
                universities.Add(ToDomain(univer));
            }
            return universities;
        }
    }
}
