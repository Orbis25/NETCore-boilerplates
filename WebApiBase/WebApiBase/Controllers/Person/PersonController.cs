using BussinesLayer.Interfaces.Person;
using DatabaseLayer.ViewModels.Inputs.Person;
using DatabaseLayer.ViewModels.VM.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBase.Controllers.Api.Core;

namespace WebApiBase.Controllers.Person
{
    [ApiController]
    public class PersonController : CoreController<IPersonService, PersonVM, PersonInput>
    {
        private readonly IPersonService _service;
        public PersonController(IPersonService service) : base(service) => _service = service;

    }
}
