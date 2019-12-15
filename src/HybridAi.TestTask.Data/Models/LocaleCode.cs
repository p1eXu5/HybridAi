using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
#nullable enable

    public class LocaleCode : IEntity
    {
        public LocaleCode( string name )
        {
            Name = name;
        }

        [StringLength(8, MinimumLength = 1)]
        [Required]
        public string Name { get; }

        public ICollection< EnCity >? EnCities { get; set; }
        public ICollection< DeCity >? DeCities { get; set; }
        public ICollection< FrCity >? FrCities { get; set; }
        public ICollection< EsCity >? EsCities { get; set; }
        public ICollection< RuCity >? RuCities { get; set; }
        public ICollection< JaCity >? JaCities { get; set; }
        public ICollection< PtBrCity>? PtBrCities { get; set; }
        public ICollection< ZhCnCity>? ZhCnCities { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

#nullable restore
}
