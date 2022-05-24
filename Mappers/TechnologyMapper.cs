using Domain;
using Entities;

namespace Mappers
{
    public static class TechnologyMapper
    {
        public static TechnologyEntity ToEntity(TechnologyDTO technology)
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

        public static TechnologyDTO ToDomain(TechnologyEntity technologyEntity)
        {
            TechnologyDTO technology = new TechnologyDTO();
            if (technologyEntity != null)
            {
                technology.Name = technologyEntity.Name;
                technology.CreatedAt = technologyEntity.CreatedAt;
                technology.Id = technologyEntity.Id;
                technology.Type = technologyEntity.Type;

                if (technologyEntity.ProjectList != null)
                    technology.ProjectList = ProjectMapper.ToDomainList(technologyEntity.ProjectList);
                else
                    technology.ProjectList = new List<ProjectDTO>();

                return technology;
            }
            return null;
        }

        public static List<TechnologyDTO> ToDomainList(List<TechnologyEntity> technologyEntity)
        {
            List<TechnologyDTO> technologies = new List<TechnologyDTO>();
            foreach (TechnologyEntity tech in technologyEntity)
            {
                technologies.Add(ToDomain(tech));
            }
            return technologies;
        }

        public static List<TechnologyEntity> ToEntityList(List<TechnologyDTO> technology)
        {
            List<TechnologyEntity> technologies = new List<TechnologyEntity>();
            foreach (TechnologyDTO tech in technology)
            {
                technologies.Add(ToEntity(tech));
            }
            return technologies;
        }
    }
}