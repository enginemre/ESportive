using SportiveOrder.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class GenericRepository<TEntity> where TEntity:class,new()
    {
        public void Add(TEntity entity)
        {
            using var _context = new SportiveOrderContext();
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            using var _context = new SportiveOrderContext();
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            using var _context = new SportiveOrderContext();
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
        public List<TEntity> GetEntities()
        {
            using var _context = new SportiveOrderContext();
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetEntity(int id)
        {
            using var _context = new SportiveOrderContext();
            return _context.Set<TEntity>().Find(id);
        }
    }
}
