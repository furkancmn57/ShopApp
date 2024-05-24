using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotFoundExcepiton : Exception
    {
        public bool Success { get; }
        public NotFoundExcepiton(string message) : base(message)
        {
            Success = false;
        }
    }
}
