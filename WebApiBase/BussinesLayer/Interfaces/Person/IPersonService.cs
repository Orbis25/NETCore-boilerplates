using BussinesLayer.Repositories.Base;
using DatabaseLayer.ViewModels.Inputs.Person;
using DatabaseLayer.ViewModels.VM.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interfaces.Person
{
    public interface IPersonService : IBaseRepository<PersonInput, PersonVM>
    {
    }
}
