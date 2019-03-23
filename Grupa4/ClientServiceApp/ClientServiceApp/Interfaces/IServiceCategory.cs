﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Interfaces
{
    interface IServiceCategory
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Category { get; set; }
    }
}
