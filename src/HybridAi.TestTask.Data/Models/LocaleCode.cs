﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class LocaleCode
    {
        public Int16 Id { get; set; }

        [StringLength(8, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        public ICollection< EnCity > EnCities { get; set; }
    }
}