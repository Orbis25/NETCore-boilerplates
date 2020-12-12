using DatabaseLayer.Models.Base;
using DatabaseLayer.Utils.Responses;
using DatabaseLayer.ViewModels.Commons.Paginated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories.Base
{
    public interface IBaseRepository<TInputModel, TEntityVM> where TEntityVM : BaseModel where TInputModel : BaseModel
    {
        Task<ResponseBase<TEntityVM>> GetPaginatedList(BasePaginated paginatedModel, Expression<Func<TEntityVM, bool>> expression = null, params Expression<Func<TEntityVM,object>>[] includes);
        Task<ResponseBase<IEnumerable<TEntityVM>>> GetList(Expression<Func<TEntityVM, bool>> expression = null, params Expression<Func<TEntityVM, object>>[] includes);
        Task<ResponseBase<IQueryable<TEntityVM>>> GetAll(Expression<Func<TEntityVM, bool>> expression = null , params Expression<Func<TEntityVM, object>>[] includes);
        Task<ResponseBase<TEntityVM>> Create(TInputModel model);
        Task<ResponseBase<TEntityVM>> Update(TInputModel model);
        Task<ResponseBase<TEntityVM>> GetById(Guid id, params Expression<Func<TEntityVM, object>>[] includes);
        Task<ResponseBase<bool>> SoftRemove(Guid id);
        Task<ResponseBase<bool>> Remove(Guid id);

    }
}
