using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public bool Success { get; }

        public int StatusCode { get; set; }
        public BusinessException(string message, int statusCode) : base(message)
        {
            Success = false;
            StatusCode = statusCode;
        }
    }
}
