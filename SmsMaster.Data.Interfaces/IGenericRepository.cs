using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;

namespace SmsMaster.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<T> FindAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
        Task<QueryResult<T>> GetPagedAsync(QueryObject queryObject, Expression<Func<T, bool>> whereCondition = null, params Expression<Func<T, object>>[] includes);

    }
}
