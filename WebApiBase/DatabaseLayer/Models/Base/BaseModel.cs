using DatabaseLayer.Enums.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyyy");
        public string UpdateAtStr => CreatedAt.ToString("dd/MM/yyyyy");
        public State State { get; set; }
    }
}
