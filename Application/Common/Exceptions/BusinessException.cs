﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public bool Success { get; }
        public BusinessException(string message) : base(message)
        {
            Success = false;
        }
    }
}
