﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FvckYeahBubbleTea.Models
{
    public class Topping:BaseClass
    {
        public float Price { get; set; }
    }
}