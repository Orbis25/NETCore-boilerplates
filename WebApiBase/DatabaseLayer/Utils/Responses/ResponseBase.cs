using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer.Utils.Responses
{
    public class ResponseBase<TEntity>
    {
        public string Code { get; set; }
        public bool IsSuccess => !ErrorMessages.Any();
        public TEntity Response { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
