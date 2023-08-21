using ProductApp.Core.UnitOfWorks;
using ProductApp.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Data.UnitOfWorks
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _context;

        public UnitOfWork(EfDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
