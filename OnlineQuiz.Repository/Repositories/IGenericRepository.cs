using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Repository.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<(IEnumerable<TEntity>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);
        Task<TEntity?> GetByIdAsync(object id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(object id);
        Task Delete(TEntity entity);
    }
}
