using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Entities;
using ProductApp.Core.Repositories;
using ProductApp.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EfDbContext context) : base(context) 
        {

        }
        public async Task<List<Product>> GetProductsByCategory(Guid categoryId)
        {
            return await base._context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
