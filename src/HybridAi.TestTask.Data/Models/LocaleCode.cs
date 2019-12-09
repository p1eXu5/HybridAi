using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
#nullable enable

    public class LocaleCode
    {
        public LocaleCode( string name )
        {
            Name = name;
        }

        [StringLength(8, MinimumLength = 1)]
        [Required]
        public string Name { get; }

        public ICollection< EnCity >? EnCities { get; set; }
    }

#nullable restore
}
