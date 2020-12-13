using BussinesLayer.Repositories.Base;
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
    public abstract class CoreController<TService, TEntityVM, TInputModel> : ControllerBase
        , ICoreController<TInputModel>
        where TInputModel : BaseModel
        where TEntityVM : BaseModel
        where TService : IBaseRepository<TInputModel, TEntityVM>
    {
        private readonly TService _service;
        public CoreController(TService service) => _service = service;

        [HttpPost]
        public virtual async Task<IActionResult> Create(TInputModel inputModel)
        {
            var response = await _service.Create(inputModel);
            if (!response.IsSuccess) return BadRequest(response.ErrorMessage);
            return CreatedAtAction(nameof(GetById), new { id = inputModel.Id }, response.Response);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.SoftRemove(id);
            if (!response.IsSuccess) return BadRequest(response.ErrorMessage);
            return Ok(response.IsSuccess);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery]BasePaginated paginatedVM) => Ok(await _service.GetPaginatedList(paginatedVM));

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var response = await _service.GetById(id);
            if (response == null) return NotFound("Not Found");
            return Ok(response);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TInputModel inputModel)
        {
            var response = await _service.Update(inputModel);
            if (!response.IsSuccess) return BadRequest(response.ErrorMessage);
            return Ok(response.Response);
        }
    }
}
