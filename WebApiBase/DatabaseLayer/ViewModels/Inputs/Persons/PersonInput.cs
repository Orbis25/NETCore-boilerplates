using DatabaseLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.ViewModels.Inputs.Person
{
    public class PersonInput : BaseModel
    {
        public string Name { get; set; }
    }
}
