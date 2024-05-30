using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Tools
{
    public class Tools
    {
        public string GenerateAuthUserName(int userId)
        {
            string authUserName = $"session_{userId}";

            return authUserName;
        }
    }
}
