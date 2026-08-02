using AppFrame.Core.AppFrameRepository;
using AppFrame.Entity.EntityBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameRepository
{
    public class RepositoryBase<TEntity> : IAppRepository<TEntity> where TEntity : AppEntityBase, new()
    {
        private readonly SqlSugarClient _db;
        public RepositoryBase()
        {
            _db = SqlSugarFactory.Create();
            _db.DbMaintenance.CreateDatabase();//建库
            _db.CodeFirst.InitTables(typeof(TEntity));//建表
        }
        public bool Delete(int id)
        {
            return _db.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
        }

        public bool Delete(TEntity entity)
        {
            return _db.Deleteable<TEntity>().Where(entity).ExecuteCommand() > 0;
        }

        public bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Deleteable<TEntity>().Where(predicate).ExecuteCommand() > 0;
        }

        public bool Insert(TEntity entity)
        {
            return _db.Insertable(entity).ExecuteCommand() > 0;
        }

        public bool Insert(List<TEntity> entities)
        {
            return _db.Insertable(entities).ExecuteCommand() > 0;
        }

        public bool InsertNa<TChild>(List<TEntity> entities, Expression<Func<TEntity, TChild>> include) where TChild : class, new()
        {
            return _db.InsertNav(entities).Include(include, new InsertNavOptions() { ManyToManyNoDeleteMap = true }).ExecuteCommand();
        }

        public TEntity Query(int id)
        {
            return _db.Queryable<TEntity>().InSingle(id);
        }

        public TEntity Query(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Queryable<TEntity>().First(predicate);
        }

        public List<TEntity> QueryAll()
        {
            
            return _db.Queryable<TEntity>().ToList();
        }

        public List<TEntity> QueryAll(Expression<Func<TEntity, bool>> predicate)
        {
            _db.Queryable<TEntity>().Includes(p => p.CreateTime);
            return _db.Queryable<TEntity>().Where(predicate).ToList();
        }

        public List<TEntity> QueryInclude<T>(Expression<Func<TEntity, T>> predicate)
        {
            return _db.Queryable<TEntity>().Includes(predicate).ToList();
        }

        public List<TEntity> QueryPage(int pageIndex, int pageSize, ref int totalCount)
        {
            return _db.Queryable<TEntity>().ToPageList(pageIndex, pageSize, ref totalCount);
        }
        /// <summary>
        /// 更新[实体需要带主键]
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            return _db.Updateable(entity).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 更新[实体需要带主键]
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool Update(List<TEntity> entities)
        {
            return _db.Updateable(entities).ExecuteCommand() > 0;
        }
    }
}
