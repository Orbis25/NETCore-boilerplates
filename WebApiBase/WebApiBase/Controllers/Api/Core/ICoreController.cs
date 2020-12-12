using DatabaseLayer.Models.Base;
using DatabaseLayer.ViewModels.Commons.Paginated;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBase.Controllers.Api.Core
{
    public interface ICoreController<TInputModel> where TInputModel : BaseModel
    {
        Task<IActionResult> Create(TInputModel inputModel);
        Task<IActionResult> Update(TInputModel inputModel);
        Task<IActionResult> Delete(Guid id);
        Task<IActionResult> GetAll(BasePaginated paginatedVM);
        Task<IActionResult> GetById(Guid id);
    }
}
