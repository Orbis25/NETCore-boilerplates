using DatabaseLayer.Models.Base;
using DatabaseLayer.ViewModels.Commons.Paginated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBase.Controllers.Api.Core
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class CoreController<TService, TInputModel> : ControllerBase
        , ICoreController<TInputModel> where TService : class where TInputModel : BaseModel
    {
        private readonly TService _service;
        public CoreController(TService service) => _service = service;

        [HttpPost]
        public virtual async Task<IActionResult> Create(TInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll(BasePaginated paginatedVM)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
