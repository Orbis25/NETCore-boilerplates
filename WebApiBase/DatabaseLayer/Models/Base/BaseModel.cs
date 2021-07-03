using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Models.Base
{
    public abstract class BaseModel
    {
        /**
         * TODO: Only postgress
         */
        [DataType("uuid")]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyyy hh:mm:ss");
        public string UpdateAtStr => CreatedAt.ToString("dd/MM/yyyyy hh:mm:ss");
        public bool IsDeleted { get; set; } = false;
    }
}
