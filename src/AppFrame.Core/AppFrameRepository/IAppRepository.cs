using AppFrame.Entity.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameRepository
{
    public interface IAppRepository<TEntity> where TEntity : AppEntityBase, new()
    {
        bool Insert(TEntity entity);
        bool Insert(List<TEntity> entities);
        bool Delete(int id);
        bool Delete(TEntity entity);
        bool Delete(Expression<Func<TEntity, bool>> predicate);
        bool Update(TEntity entity);
        bool Update(List<TEntity> entities);
        TEntity Query(int id);
        TEntity Query(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> QueryAll();
        List<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> QueryPage(int pageIndex, int pageSize, ref int totalCount);
        List<TEntity> QueryInclude<T>(Expression<Func<TEntity, T>> predicate);
        bool InsertNa<TChild>(List<TEntity> entities, Expression<Func<TEntity, TChild>> include) where TChild :class,new();
    }
}
