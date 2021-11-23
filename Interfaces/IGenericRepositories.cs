using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Interfaces
{
    public interface IGenericRepositories<TEntity> where TEntity:class,new ()
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

        List<TEntity> GetEntities();

        TEntity GetEntity(int id);
    }
}
