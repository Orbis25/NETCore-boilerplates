using AutoMapper;
using BussinesLayer.Services.Persons;
using DatabaseLayer.Contexts;
using DatabaseLayer.Mappings.Base;
using DatabaseLayer.Models.Persons;
using DatabaseLayer.ViewModels.Inputs.Person;
using DatabaseLayer.ViewModels.VM.Person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiBase.Test.BaseConfiguration;

namespace WebApiBase.Test.TestedServices
{
    [TestClass]
    public class PersonTest : CoreTest<ApplicationDbContext,Person,PersonInput,PersonVM>
    {
        public PersonTest()
        {
            Initialize();
        }

        protected override void Initialize()
        {
           
            //assert
            var database = new ApplicationDbContext(GetDbOptions());
            database.Persons.AddRange(GetFakeDataList());
            database.SaveChanges();
            //arange
            var profiles = new List<Profile> { new CommonMapping() };
            var mapper = SetUpMapper(profiles.ToArray());
            //act
            _service = new PersonService(database, mapper);
        }

      
       

     
    }
}
