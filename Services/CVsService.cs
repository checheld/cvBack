using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class CVsService : ICVsService
    {
        private readonly ICVsRepository _CVsRepository;

        public CVsService(IServiceProvider _serviceProvider)
        {
            _CVsRepository = _serviceProvider.GetService<ICVsRepository>();
        }

        public async Task<CVDTO> AddCV(CVDTO cv)
        {
            try
            {
                CVEntity newCV = CVMapper.ToEntity(cv);
                CVEntity u = await _CVsRepository.AddCV(newCV);
                if (u != null)
                {
                    CVDTO item = CVMapper.ToDomain(u);
                    return item;
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }

        public async Task<string> DeleteCVById(int id)
        {
            try
            {
                var getCV = await this._CVsRepository.GetCVById(id);

                if (getCV != null)
                {
                    await this._CVsRepository.RemoveAllProjectCVs(getCV.Id);
                }

                return await _CVsRepository.DeleteCVById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<CVDTO> GetCVById(int id)
        {
            try
            {
                return CVMapper.ToDomain(await _CVsRepository.GetCVById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CVDTO>> GetCVsBySearch(string search)
        {
            try
            {
                return CVMapper.ToDomainList(await _CVsRepository.GetCVsBySearch(search));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CVDTO> UpdateCV(CVDTO cv)
        {
            try
            {
                var getCV = await this._CVsRepository.GetCVById(cv.Id);

                await _CVsRepository.Update(getCV);
                await _CVsRepository.UpdateCV(CVMapper.ToEntity(cv));

                await this._CVsRepository.RemoveAllProjectCVs(getCV.Id);

                return CVMapper.ToDomain(await _CVsRepository.UpdateCV(CVMapper.ToEntity(cv)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<CVDTO>> GetAllCVs()
        {
            try
            {
                List<CVEntity> CVEntityList = await _CVsRepository.GetAllCVs();
                List<CVDTO> CVDomainList = new List<CVDTO>();
                CVEntityList.ForEach(x => CVDomainList.Add(CVMapper.ToDomain(x)));
                return CVDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}