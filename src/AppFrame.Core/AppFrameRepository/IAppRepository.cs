using AppFrame.Entity.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameRepository
{
    public interface IAppRepository<TEntity> where TEntity : AppEntityBase, new()  // TEntity must inherit from AppEntityBase and have a parameterless constructor
    {
        bool Insert(TEntity entity);
        bool Insert(List<TEntity> entities);

        bool Delete(int id);                                     // Delete an entity by its ID
        bool Delete(TEntity entity);                            // Delete the given entity
        bool Delete(Expression<Func<TEntity, bool>> predicate); // Delete entities that match the given predicate ,表达式树

        bool Update(TEntity entity);                            // 单条更新
        bool Update(List<TEntity> entities);                    // 批量更新
        TEntity Query(int id);
        TEntity Query(Expression<Func<TEntity, bool>> predicate);  // Query a single entity that matches the given predicate
        List<TEntity> QueryAll();
        List<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> QueryPage(int pageIndex, int pageSize, ref int totalCount);

        List<TEntity> QueryInclude<T>(Expression<Func<TEntity, T>> predicate);


        bool InsertNa<TChild>(List<TEntity> entities, Expression<Func<TEntity, TChild>> include) where TChild :class,new();
    }
}
