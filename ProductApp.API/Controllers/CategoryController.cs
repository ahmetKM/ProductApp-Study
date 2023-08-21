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
    public class CategoryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;
        public CategoryController(IMapper mapper,  ICategoryService categoryService)
        {
            _mapper = mapper;
            _service = categoryService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All()
        {
            var categories = await _service.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            return CreateActionResult<List<CategoryDto>>(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return CreateActionResult<CategoryDto>(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _service.AddAsync(_mapper.Map<Category>(categoryDto));
            var categoriesDto = _mapper.Map<CategoryDto>(category);
            return CreateActionResult<CategoryDto>(CustomResponseDto<CategoryDto>.Success(201, categoriesDto));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Category>(categoryUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _service.GetByIdAsync(id);
            var response = category == null ? CustomResponseDto<NoContentDto>.Fail(404, "Given category cannot found") : CustomResponseDto<NoContentDto>.Success(204);
            await _service.RemoveAsync(category);
            return CreateActionResult(response);
        }
    }
}
