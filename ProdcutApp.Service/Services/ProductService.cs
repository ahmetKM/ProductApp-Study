using AutoMapper;
using ProductApp.Core.DTOs;
using ProductApp.Core.Entities;
using ProductApp.Core.Repositories;
using ProductApp.Core.Services;
using ProductApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository,IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository): base(repository, unitOfWork) 
        {
            _productRepository = productRepository;
        }
        
        public async Task<CustomResponseDto<List<ProductDto>>> GetProductsByCategory(Guid CategoryId)
        {
            var products = await _productRepository.GetProductsByCategory(CategoryId);
            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return CustomResponseDto<List<ProductDto>>.Success(200, productsDto);
        }
    }
}
