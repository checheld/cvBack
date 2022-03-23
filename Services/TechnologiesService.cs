using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class TechnologiesService : ITechnologiesService
    {
        private readonly ITechnologiesRepository _technologiesRepository;
        public TechnologiesService(IServiceProvider _serviceProvider)
        {
            _technologiesRepository = _serviceProvider.GetService<ITechnologiesRepository>();
        }

        public async Task<Technology> AddTechnology(Technology technology)
        {
            try
            {
                TechnologyEntity newTechnology = TechnologyMapper.ToEntity(technology);
                TechnologyEntity u = await _technologiesRepository.AddTechnology(newTechnology);
                if (u != null)
                {
                    Technology item = TechnologyMapper.ToDomain(u);
                    return item;
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }

        public async Task<string> DeleteTechnologyById(int id)
        {
            try
            {
                return await _technologiesRepository.DeleteTechnologyById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Technology> GetTechnologyById(int id)
        {
            try
            {
                return TechnologyMapper.ToDomain(await _technologiesRepository.GetTechnologyById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Technology>> GetTechnologiesBySearch(string search)
        {
            try
            {
                return TechnologyMapper.ToDomainList(await _technologiesRepository.GetTechnologiesBySearch(search));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Technology> UpdateTechnology(Technology technology)
        {
            try
            {
                return TechnologyMapper.ToDomain(await _technologiesRepository.UpdateTechnology(TechnologyMapper.ToEntity(technology)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Technology>> GetAllTechnologies()
        {
            try
            {
                List<TechnologyEntity> technologyEntityList = await _technologiesRepository.GetAllTechnologies();
                List<Technology> technologyDomainList = new List<Technology>();
                technologyEntityList.ForEach(x => technologyDomainList.Add(TechnologyMapper.ToDomain(x)));
                return technologyDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
