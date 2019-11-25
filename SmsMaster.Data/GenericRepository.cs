using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SmsMaster.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected SmsMasterContext _context;
        protected DbSet<T> _set;
        protected Dictionary<string, Expression<Func<T, object>>> columnsMap;
        public GenericRepository(SmsMasterContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<QueryResult<T>> GetPagedAsync(QueryObject queryObject, Expression<Func<T, bool>> whereCondition = null, params Expression<Func<T, object>>[] includes)
        {
            var result = new QueryResult<T>();

            IQueryable<T> query = _set;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            ApplyOrdering(query, queryObject, columnsMap);

            result.TotalCount = await query.CountAsync();
            result.Items = await query.Skip(queryObject.Skip).Take(queryObject.Take).ToListAsync();
            return result;
        }

        protected void ApplyOrdering(IQueryable<T> query, QueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnsMap.ContainsKey(queryObject.SortBy))
                return ;

            if (queryObject.IsSortAscending)
                 query.OrderBy(columnsMap[queryObject.SortBy]);
            else
                 query.OrderByDescending(columnsMap[queryObject.SortBy]);
        }
    }
}
