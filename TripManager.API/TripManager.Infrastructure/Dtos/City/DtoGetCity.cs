﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripManager.Infrastructure.Dtos
{
    public class DtoGetCity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
