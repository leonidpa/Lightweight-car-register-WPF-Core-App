﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarsRepositoryLibrary.Models
{
    public class Car
    {
        public int? Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Owner { get; set; }
    }
}
