﻿using ProductApp.Core.DTOs;
using ProductApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Core.Services
{
    public interface IProductService: IService<Product>
    {
        Task <CustomResponseDto<List<ProductDto>>> GetProductsByCategory(Guid CategoryId);
    }
}
