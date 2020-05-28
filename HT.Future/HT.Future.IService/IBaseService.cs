using HT.Future.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HT.Future.IService
{
    /// <summary>
    /// 数据库服务基类
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    public interface IBaseService<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// 数据库实体集合
        /// </summary>
        DbSet<TEntity> Entities { get; }
        /// <summary>
        /// 数据库表
        /// </summary>
        IQueryable<TEntity> Table { get; }
        /// <summary>
        /// 非跟踪的数据库表
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }
        /// <summary>
        /// 添加实体对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="saveNow">是否直接保存</param>
        void Add(TEntity entity, bool saveNow = true);
        /// <summary>
        /// 添加实体对象（异步）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="saveNow">是否直接保存</param>
        Task AddAsync(TEntity entity, bool saveNow = true);
        /// <summary>
        /// 批量添加实体对象
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveNow"></param>
        void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
        /// <summary>
        /// 批量添加实体对象（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveNow"></param>
        Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        /// <summary>
        /// 跟踪实体对象
        /// </summary>
        /// <param name="entity"></param>
        void Attach(TEntity entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveNow"></param>
        void Delete(TEntity entity, bool saveNow = true);
        /// <summary>
        /// 删除实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveNow"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity, bool saveNow = true);
        /// <summary>
        /// 批量删除实体对象
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveNow"></param>
        void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
        /// <summary>
        /// 批量删除实体对象（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveNow"></param>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);
        /// <summary>
        /// 取消跟踪实体
        /// </summary>
        /// <param name="entity"></param>
        void Detach(TEntity entity);
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        TEntity GetById(params object[] ids);
        /// <summary>
        /// 根据id获取实体（异步）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        ValueTask<TEntity> GetByIdAsync(params object[] ids);
        /// <summary>
        /// 加载指定的集合属性
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="collectionProperty"></param>
        void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        /// <summary>
        /// 加载指定的集合属性（异步）
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="collectionProperty"></param>
        /// <returns></returns>
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        /// <summary>
        /// 加载指定的导航属性
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="referenceProperty"></param>
        void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        /// <summary>
        /// 加载指定的导航属性（异步）
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="referenceProperty"></param>
        /// <returns></returns>
        Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveNow"></param>
        void Update(TEntity entity, bool saveNow = true);
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="fileds">需要更新的字段</param>
        /// <param name="saveNow">是否立即持久化</param>
        /// <returns></returns>
        void Update(TEntity entity, IEnumerable<string> fileds, bool saveNow = true);
        /// <summary>
        /// 更新实体对象（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveNow"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity, bool saveNow = true);
        /// <summary>
        /// 更新实体对象（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="fileds">需要更新的字段</param>
        /// <param name="saveNow">是否立即持久化</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity, IEnumerable<string> fileds, bool saveNow = true);
        /// <summary>
        /// 批量更新实体对象
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveNow"></param>
        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
        /// <summary>
        /// 批量更新实体对象
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveNow"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true);

    }
}
