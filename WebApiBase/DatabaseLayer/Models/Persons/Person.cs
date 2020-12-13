using DatabaseLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models.Persons
{
    public class Person : BaseModel
    {
        public string Name { get; set; }
    }
}
