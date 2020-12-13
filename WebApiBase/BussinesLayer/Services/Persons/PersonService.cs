using AutoMapper;
using BussinesLayer.Interfaces.Person;
using BussinesLayer.Repositories.Base;
using DatabaseLayer.Contexts;
using DatabaseLayer.Models.Persons;
using DatabaseLayer.ViewModels.Inputs.Person;
using DatabaseLayer.ViewModels.VM.Person;

namespace BussinesLayer.Services.Persons
{
    public class PersonService : BaseRepository<Person, ApplicationDbContext, PersonInput, PersonVM>, IPersonService
    {
        public PersonService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
