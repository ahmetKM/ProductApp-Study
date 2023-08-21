using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductApp.Core.DTOs;
using ProductApp.Core.Entities;
using ProductApp.Core.Repositories;
using ProductApp.Core.Services;
using ProductApp.Core.UnitOfWorks;
using ProductApp.Data.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        static List<Category> categories => new()
    {
        new Category { Id = new Guid("9542a4ff-0175-4826-8861-d4ea1bc0ffb4"), Name = "Giyim" },
        new Category { Id = new Guid("47d782e8-363c-4081-b222-a68986996427"), Name = "Ayakkabı"},
        new Category { Id = new Guid("570bcae6-ff65-40cc-b9dd-395acbc05559"), Name = "Giyim" },
        new Category { Id = new Guid("570bcae6-ff65-40cc-b9dd-395acbc05559"), Name = "Ayakkabı"}
    };

        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitMQConsumerService _rabbitMQCosnumerService;

        public CategoryService(IGenericRepository<Category> repository,IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, IRabbitMQConsumerService rabbitMQCosnumerService) : base(repository, unitOfWork) 
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _rabbitMQCosnumerService = rabbitMQCosnumerService;
        }

        public async override Task<IEnumerable<Category>> GetAllAsync()
        {
            var redisData = GetAllCategoriesFromRedis();
            return redisData.Count > 0 ? redisData : await _repository.GetAll().ToListAsync();
        }

        public async override Task<Category> AddAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            await _rabbitMQCosnumerService.SendNewCategoryMailToQueue();

            return category;
        }

        private List<Category> GetAllCategoriesFromRedis() 
        {
            return _cacheService.GetOrAdd("Categories", () => { return categories; });
        }

    }
}
