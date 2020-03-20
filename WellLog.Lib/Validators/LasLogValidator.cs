using System;
using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Validators
{
    public class LasLogValidator : ILasLogValidator
    {
        public IEnumerable<ValidationError> ValidateLasLog(params LasLog[] lasLogs)
        {
            if (lasLogs == null) { throw new ArgumentNullException(nameof(lasLogs)); }

            var response = new List<ValidationError>();

            /* version information */
            response.AddRange(lasLogs.Where(x => x.Sections == null || x.Sections.First().SectionType != LasSectionType.VersionInformation).Select(x => new ValidationError(x, nameof(x.Sections), "The ~V section is mandatory and must appear as the first section.")));
            response.AddRange(lasLogs.Where(x => x.SectionCount(LasSectionType.VersionInformation) > 1).Select(x => new ValidationError(x, nameof(x.Sections), "Only one ~V section can occur.")));
            response.AddRange(lasLogs.Where(x => x.VersionInformation != null && !x.VersionInformation.HasVersionMnemonic()).Select(x => new ValidationError(x, nameof(x.VersionInformation), "The ~V section must contain a VERS mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.VersionInformation != null && !x.VersionInformation.HasWrapMnemonic()).Select(x => new ValidationError(x, nameof(x.VersionInformation), "The ~V section must contain a WRAP mnemonic.")));

            /* well information */
            response.AddRange(lasLogs.Where(x => x.SectionCount(LasSectionType.WellInformation) != 1).Select(x => new ValidationError(x, nameof(x.Sections), "The ~W section is mandatory and can occur only once.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasStartMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a STRT mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasStopMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a STOP mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasStepMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a STEP mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasNullMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a NULL mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasCompanyMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a COMP mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasWellMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a WELL mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasFieldMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a FLD mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasLocationMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a LOC mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasAreaMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a PROV, CNTY, STAT, or CTRY mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasServiceCompanyMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a SRVC mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasDateMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a DATE mnemonic.")));
            response.AddRange(lasLogs.Where(x => x.WellInformation != null && !x.WellInformation.HasIdentifierMnemonic()).Select(x => new ValidationError(x, nameof(x.WellInformation), "The ~W section must contain a UWI or API mnemonic.")));

            /* curve information */
            response.AddRange(lasLogs.Where(x => x.SectionCount(LasSectionType.CurveInformation) != 1).Select(x => new ValidationError(x, nameof(x.Sections), "The ~C section is mandatory and can occur only once.")));
            response.AddRange(lasLogs.Where(x => x.CurveInformation != null & !x.CurveInformation.HasIndexChannel()).Select(x => new ValidationError(x, nameof(x.CurveInformation), "The only valid mnemonics for the index channel in the ~C section are DEPT, DEPTH, TIME or INDEX.")));

            /* parameter information */
            response.AddRange(lasLogs.Where(x => x.SectionCount(LasSectionType.ParameterInformation) > 1).Select(x => new ValidationError(x, nameof(x.Sections), "Only one ~P section can occur.")));

            /* other information */
            response.AddRange(lasLogs.Where(x => x.SectionCount(LasSectionType.OtherInformation) > 1).Select(x => new ValidationError(x, nameof(x.Sections), "Only one ~O section can occur.")));

            /* ascii log data */
            response.AddRange(lasLogs.Where(x => x.Sections == null || x.Sections.Last().SectionType != LasSectionType.AsciiLogData).Select(x => new ValidationError(x, nameof(x.Sections), "The ~A section is mandatory and must appear as the last section.")));
            response.AddRange(lasLogs.Where(x => x.SectionCount(LasSectionType.AsciiLogData) > 1).Select(x => new ValidationError(x, nameof(x.Sections), "Only one ~A section can occur.")));
            response.AddRange(lasLogs.Where(x => x.AsciiLogData != null && x.AsciiLogData.EmptyAsciiLogDataLineCount() > 0).Select(x => new ValidationError(x, nameof(x.AsciiLogData), "Embedded blank lines anywhere in the ~A section are forbidden.")));
            response.AddRange(lasLogs.Where(x => !x.AsciiLogDataHasCurveChannels()).Select(x => new ValidationError(x, nameof(x.AsciiLogData), "The channels described the ~C section must be present in the data set.")));

            return response.ToArray();
        }
    }
}
