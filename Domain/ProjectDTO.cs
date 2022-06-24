﻿using Domain.Abstract;

namespace Domain
{
    public class ProjectDTO : BaseDomain
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Link { get; set; }
        public virtual List<ProjectPhotoDTO>? PhotoList { get; set; }
        public virtual List<TechnologyDTO>? TechnologyList { get; set; }
        public virtual List<ProjectCVDTO>? CVProjectCVList { get; set; }
    }
}