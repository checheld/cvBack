using Domain;
using Entities;

namespace Mappers
{
    public static class TechnologyMapper
    {
        public static TechnologyEntity ToEntity(Technology technology)
        {
            TechnologyEntity technologyEntity = new TechnologyEntity();

            if (technology != null)
            {
                technologyEntity.Name = technology.Name;
                technologyEntity.Id = technology.Id;
                technologyEntity.CreatedAt = DateTime.Now;
                technologyEntity.Type = technology.Type;
                return technologyEntity;
            }
            return null;
        }

        public static Technology ToDomain(TechnologyEntity technologyEntity)
        {
            Technology technology = new Technology();
            if (technologyEntity != null)
            {
                technology.Name = technologyEntity.Name;
                technology.CreatedAt = technologyEntity.CreatedAt;
                technology.Id = technologyEntity.Id;
                technology.Type = technologyEntity.Type;

                return technology;
            }
            return null;
        }

        public static List<Technology> ToDomainList(List<TechnologyEntity> technologyEntity)
        {
            List<Technology> technologies = new List<Technology>();
            foreach (TechnologyEntity tech in technologyEntity)
            {
                technologies.Add(ToDomain(tech));
            }
            return technologies;
        }
    }
}