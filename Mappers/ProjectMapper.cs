using Domain;
using Entities;

namespace Mappers
{
    public static class ProjectMapper
    {
        public static ProjectEntity ToEntity(ProjectDTO project)
        {
            ProjectEntity projectEntity = new ProjectEntity();
            if (project != null)
            {
                projectEntity.Name = project.Name;
                projectEntity.Description = project.Description;
                projectEntity.Type = project.Type;
                projectEntity.Country = project.Country;
                projectEntity.Link = project.Link;
                projectEntity.Id = project.Id;
                /*projectEntity.Technology = project.Technology;*/
                projectEntity.CreatedAt = DateTime.Now;

                if (project.TechnologyList != null)
                    projectEntity.TechnologyList = TechnologyMapper.ToEntityList(project.TechnologyList);
                else
                    projectEntity.TechnologyList = new List<TechnologyEntity>();

                return projectEntity;
            }
            return null;
        }

        public static ProjectDTO ToDomain(ProjectEntity projectEntity)
        {
            ProjectDTO project = new ProjectDTO();
            if (projectEntity != null)
            {
                project.Name = projectEntity.Name;
                project.Description = projectEntity.Description;
                project.Type = projectEntity.Type;
                project.Country = projectEntity.Country;
                project.Link = projectEntity.Link;
                project.CreatedAt = projectEntity.CreatedAt;
                /*project.Technology = projectEntity.Technology;*/
                project.Id = projectEntity.Id;

                if (projectEntity.TechnologyList != null)
                    project.TechnologyList = TechnologyMapper.ToDomainList(projectEntity.TechnologyList);
                else
                    project.TechnologyList = new List<TechnologyDTO>();

                return project;
            }
            return null;
        }

        public static List<ProjectDTO> ToDomainList(List<ProjectEntity> projectEntity)
        {
            List<ProjectDTO> projects = new List<ProjectDTO>();
            foreach (ProjectEntity project in projectEntity)
            {
                projects.Add(ToDomain(project));
            }
            return projects;
        }

        public static List<ProjectEntity> ToEntityList(List<ProjectDTO> project)
        {
            List<ProjectEntity> projects = new List<ProjectEntity>();
            foreach (ProjectDTO proj in project)
            {
                projects.Add(ToEntity(proj));
            }
            return projects;
        }
    }
}


