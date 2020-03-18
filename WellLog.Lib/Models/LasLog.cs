using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Models
{
    public class LasLog
    {
        public IEnumerable<LasSection> Sections { get; set; }

        [Key]
        public string WellIdentifier => this.WellIdentifier();
    }
}
