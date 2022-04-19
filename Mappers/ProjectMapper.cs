using Domain;
using Entities;

namespace Mappers
{
    public static class ProjectMapper
    {
        public static ProjectEntity ToEntity(Project project)
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

        public static Project ToDomain(ProjectEntity projectEntity)
        {
            Project project = new Project();
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
                    project.TechnologyList = new List<Technology>();

                return project;
            }
            return null;
        }

        public static List<Project> ToDomainList(List<ProjectEntity> projectEntity)
        {
            List<Project> projects = new List<Project>();
            foreach (ProjectEntity project in projectEntity)
            {
                projects.Add(ToDomain(project));
            }
            return projects;
        }

        public static List<ProjectEntity> ToEntityList(List<Project> project)
        {
            List<ProjectEntity> projects = new List<ProjectEntity>();
            foreach (Project proj in project)
            {
                projects.Add(ToEntity(proj));
            }
            return projects;
        }
    }
}


