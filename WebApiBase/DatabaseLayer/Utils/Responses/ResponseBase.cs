using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer.Utils.Responses
{
    public class ResponseBase<TEntity>
    {
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public TEntity Response { get; set; }
        public string ErrorMessage { get; set; }
    }
}
