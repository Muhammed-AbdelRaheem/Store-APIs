using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity)==typeof(Product))
            {
              return (IEnumerable<TEntity>)  await _context.Products.Include(P=>P.Brand).Include(P => P.Type).ToListAsync();

            }

          return  await   _context.Set<TEntity>().ToListAsync();

        }

        public async Task<TEntity> GetAsync(TKey Id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return  await _context.Products.Where(P => P.Id == Id as int?).Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync()as TEntity;

            }

            return await _context.Set<TEntity>().FindAsync(Id);

        }
        public async Task AddAsync(TEntity entity)
        {
           await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }




    }
}
