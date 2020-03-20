using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Models
{
    public class LasLog
    {
        public IEnumerable<LasSection> Sections { get; set; }

        [Key]
        public string WellIdentifier => this.WellIdentifier();

        public LasSection VersionInformation => this.GetSection(LasSectionType.VersionInformation);
        public LasSection WellInformation => this.GetSection(LasSectionType.WellInformation);
        public LasSection CurveInformation => this.GetSection(LasSectionType.CurveInformation);
        public LasSection ParameterInformation => this.GetSection(LasSectionType.ParameterInformation);
        public LasSection OtherInformation => this.GetSection(LasSectionType.OtherInformation);
        public LasSection AsciiLogData => this.GetSection(LasSectionType.AsciiLogData);
    }
}
