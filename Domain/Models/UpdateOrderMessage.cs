﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UpdateOrderMessage
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
