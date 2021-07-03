using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseLayer.Models.Base;
using DatabaseLayer.Utils.Paginated;
using DatabaseLayer.Utils.Responses;
using DatabaseLayer.ViewModels.Commons.Paginated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories.Base
{
    public abstract class BaseRepository<TEntity, TContext, TInputModel, TEntityVM> : IBaseRepository<TInputModel, TEntityVM>
        where TContext : DbContext
        where TEntity : BaseModel
        where TInputModel : BaseModel
        where TEntityVM : BaseModel
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;
        public BaseRepository(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<string> CommitAsync()
        {

            string err = string.Empty;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                err = (ex.InnerException.Message ?? ex.Message);
                await transaction.RollbackAsync();
            }
            return err;
        }

        public virtual async Task<ResponseBase<TEntityVM>> Create(TInputModel model)
        {
            var _model = _mapper.Map<TInputModel, TEntity>(model);
            _context.Set<TEntity>().Add(_model);
            return new ResponseBase<TEntityVM>
            {
                Response = _mapper.Map<TEntity, TEntityVM>(_model),
                ErrorMessage = await CommitAsync()
            };
        }

        public virtual IQueryable<TEntityVM> GetAll(Expression<Func<TEntityVM, bool>> expression = null,
            bool orderDesc = true, Expression<Func<TEntityVM, object>> ordered = null
            , params Expression<Func<TEntityVM, object>>[] includes)
        {
            var results = _context.Set<TEntity>().ProjectTo<TEntityVM>(_mapper.ConfigurationProvider);
            if (expression != null)
                results = results.Where(expression);
            foreach (var include in includes) results = results.Include(include);

            ///Order elements desc or asc
            if (ordered != null && orderDesc) results = results.OrderByDescending(ordered);
            if (!orderDesc && ordered != null) results = results.OrderBy(ordered);
            if (orderDesc) results = results.OrderByDescending(x => x.CreatedAt);
            else results = results.OrderBy(x => x.CreatedAt);
            return results.AsNoTracking();
        }

        public virtual async Task<BasePaginationResult<TEntityVM>> GetPaginatedList(BasePaginated paginatedModel,
            Expression<Func<TEntityVM, bool>> expression = null,
            bool orderDesc = true,
            Expression<Func<TEntityVM, object>> ordered = null,
            params Expression<Func<TEntityVM, object>>[] includes)
        {
            var results = GetAll(expression, orderDesc, ordered, includes);
            var total = results.Count();
            var pages = total / paginatedModel.Qyt;
            results = results.Take(paginatedModel.Qyt).Skip((paginatedModel.Page - 1) * paginatedModel.Qyt);
            return new BasePaginationResult<TEntityVM>
            {
                ActualPage = paginatedModel.Page,
                Qyt = paginatedModel.Qyt,
                PageTotal = pages,
                Total = total,
                Results = await results.ToListAsync()
            };

        }

        public virtual async Task<IEnumerable<TEntityVM>> GetList(Expression<Func<TEntityVM, bool>> expression = null,
            bool orderDesc = true,
            Expression<Func<TEntityVM, object>> ordered = null,
            params Expression<Func<TEntityVM, object>>[] includes)
            => await GetAll(expression, orderDesc, ordered, includes).ToListAsync();

        public virtual async Task<ResponseBase<TEntityVM>> Update(TInputModel model)
        {
            var exist = await Exist(model.Id);
            if (!exist)
            {
                return new ResponseBase<TEntityVM>
                {
                    ErrorMessage = "Not exist"
                };
            }

            var _model = _mapper.Map<TInputModel, TEntity>(model);
            _context.Set<TEntity>().Update(_model);
            return new ResponseBase<TEntityVM>
            {
                Response = _mapper.Map<TEntity, TEntityVM>(_model),
                ErrorMessage = await CommitAsync()
            };
        }

        public virtual async Task<TEntityVM> GetById(Guid id, bool asNotTraking = false, params Expression<Func<TEntityVM, object>>[] includes)
        {
            var results = GetAll(null, true, x => x.CreatedAt, includes);
            if (asNotTraking) results = results.AsNoTracking();
            return await results.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<ResponseBase<bool>> SoftRemove(Guid id)
        {
            var entity = _mapper.Map<TEntityVM, TEntity>(await GetById(id));
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<TEntity>().Update(entity);
                return new ResponseBase<bool>
                {
                    ErrorMessage = await CommitAsync()
                };
            }
            return new ResponseBase<bool>
            {
                ErrorMessage = "not found"
            };
        }

        public virtual async Task<ResponseBase<bool>> Remove(Guid id)
        {
            var entity = _mapper.Map<TEntityVM, TEntity>(await GetById(id));
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                return new ResponseBase<bool>
                {
                    ErrorMessage = await CommitAsync()
                };
            }

            return new ResponseBase<bool>
            {
                ErrorMessage = "Not found"
            };
        }

        public virtual async Task<bool> Exist(Guid id) => await GetById(id, true) != null;

        public async Task<int> Count(params Expression<Func<TEntityVM, bool>>[] expression)
        {
            var result = _context.Set<TEntity>().ProjectTo<TEntityVM>(_mapper.ConfigurationProvider)
                .AsQueryable();
            foreach (var item in expression) result = result.Where(item);
            return await result.CountAsync();
        }

        public async Task<bool> Exist(Expression<Func<TEntityVM, bool>> expression)
        {
            var result = _context.Set<TEntity>().ProjectTo<TEntityVM>(_mapper.ConfigurationProvider);
            return await result.AnyAsync(expression);
        }
    }
}
