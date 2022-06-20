using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using AutoMapper;

namespace Services
{
    public class CVsService : ICVsService
    {
        private readonly ICVsRepository _CVsRepository;
        private readonly IMapper _mapper;

        public CVsService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            _mapper = mapper;
            _CVsRepository = _serviceProvider.GetService<ICVsRepository>();
        }

        public class AppMappingCV : Profile
        {
            public AppMappingCV()
            {
                CreateMap<CVDTO, CVEntity>().ForMember(x => x.ProjectCVList, y => y.MapFrom(t => t.ProjectCVList)).
                    ForMember(x => x.User, y => y.MapFrom(t => t.User)).ReverseMap();
                CreateMap<ProjectCVDTO, ProjectCVEntity>().ForMember(x => x.Project, y => y.MapFrom(t => t.Project)).ReverseMap();
                CreateMap<UserDTO, UserEntity>().ReverseMap();
                CreateMap<ProjectDTO, ProjectEntity>().ReverseMap();
            }
        }

        public async Task<CVDTO> AddCV(CVDTO cv)
        {
            try
            {
                /*CVEntity newCV = CVMapper.ToEntity(cv);*/
                CVEntity newCV = _mapper.Map<CVEntity>(cv);
                newCV.CreatedAt = DateTime.Now;

                var c = await _CVsRepository.AddCV(newCV);
                c.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();

                /*CVDTO item = CVMapper.ToDomain(u);*/
                var item = _mapper.Map<CVDTO>(c);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteCVById(int id)
        {
            try
            {
                var getCV = await this._CVsRepository.GetCVById(id);

                await this._CVsRepository.RemoveAllProjectCVs(getCV.Id);

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
                var cv = await this._CVsRepository.GetCVById(id);
                cv.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();

                return _mapper.Map<CVDTO>(cv);
                /*return CVMapper.ToDomain(await _CVsRepository.GetCVById(id));*/
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
                var searchCV = await _CVsRepository.GetCVsBySearch(search);
                List<CVDTO> CVs = new List<CVDTO>();
                foreach (CVEntity cv in searchCV)
                {
                    cv.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();
                    CVs.Add(_mapper.Map<CVDTO>(cv));
                }
                return CVs;
               /* return CVMapper.ToDomainList(await _CVsRepository.GetCVsBySearch(search));*/
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
                var c = await _CVsRepository.UpdateCV(_mapper.Map<CVEntity>(cv));
                c.ProjectCVList.Select(c => { c.CV = null; c.Project = null; return c; }).ToList();
                return _mapper.Map<CVDTO>(c);

                /*var getCV = await this._CVsRepository.GetCVById(cv.Id);
                return CVMapper.ToDomain(await _CVsRepository.UpdateCV(CVMapper.ToEntity(cv)));*/
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
                CVEntityList.ForEach(c => c.ProjectCVList.Select(c => { c.CV = null; c.Project.TechnologyList = null; c.Project.CVProjectCVList = null; return c; }).ToList());
                List<CVDTO> CVDomainList = new List<CVDTO>();
                CVEntityList.ForEach(x => CVDomainList.Add(_mapper.Map<CVDTO>(x)));
                return CVDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}