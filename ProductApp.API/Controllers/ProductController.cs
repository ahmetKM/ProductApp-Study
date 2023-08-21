using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.API.Filters;
using ProductApp.Core.DTOs;
using ProductApp.Core.Entities;
using ProductApp.Core.Services;

namespace ProductApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateFilterAttribute]
    public class ProductController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        public ProductController(IMapper mapper, IProductService service) 
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("category/{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductsByCategoryId(Guid id) 
        {
            return CreateActionResult(await _service.GetProductsByCategory(id));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var response = product == null ? CustomResponseDto<NoContentDto>.Fail(404, "Given product cannot found") : CustomResponseDto<NoContentDto>.Success(204);
            await _service.RemoveAsync(product);
            return CreateActionResult(response);
        }
    }
}
