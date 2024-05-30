﻿using Application.Features.Mail.Interfaces;
using Application.Features.Mail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IMailProviderFactory
    {
        IMailServiceProvider GetProvider(Settings request);
    }
}
