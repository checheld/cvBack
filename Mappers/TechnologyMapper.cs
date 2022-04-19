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

                if (technology.ProjectList != null)
                    technologyEntity.ProjectList = ProjectMapper.ToEntityList(technology.ProjectList);
                else
                    technologyEntity.ProjectList = new List<ProjectEntity>();

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

                if (technologyEntity.ProjectList != null)
                    technology.ProjectList = ProjectMapper.ToDomainList(technologyEntity.ProjectList);
                else
                    technology.ProjectList = new List<Project>();

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

        public static List<TechnologyEntity> ToEntityList(List<Technology> technology)
        {
            List<TechnologyEntity> technologies = new List<TechnologyEntity>();
            foreach (Technology tech in technology)
            {
                technologies.Add(ToEntity(tech));
            }
            return technologies;
        }
    }
}