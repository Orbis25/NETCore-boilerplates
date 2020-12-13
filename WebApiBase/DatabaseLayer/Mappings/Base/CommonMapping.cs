using AutoMapper;
using DatabaseLayer.Models.Persons;
using DatabaseLayer.ViewModels.Inputs.Person;
using DatabaseLayer.ViewModels.VM.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Mappings.Base
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<Person, PersonVM>().ReverseMap();
            CreateMap<Person, PersonInput>().ReverseMap();

        }
    }
}
