﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CheckoutDetails
    {
        public SavedAddress Address { get; set; }
        public Cart Cart { get; set; }
    }
}
