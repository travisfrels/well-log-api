using System;
using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Enumerations.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class FileHeaderLogicalRecordBusiness : IFileHeaderLogicalRecordBusiness
    {
        public const string FILE_HEADER_TYPE = "FILE-HEADER";
        public const string SEQUENCE_NUMBER_LABEL = "SEQUENCE-NUMBER";
        public const string ID_LABEL = "ID";

        public ExplicitlyFormattedLogicalRecord GetFileHeaderEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs)
        {
            if (eflrs == null) { return null; }
            return eflrs.FirstOrDefault(x => x.Set != null && string.Compare(x.Set.Type, FILE_HEADER_TYPE, true) == 0);
        }

        public FileHeaderLogicalRecord ConvertEFLRtoFileHeader(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (string.Compare(eflr.Set.Type, FILE_HEADER_TYPE, true) != 0) { return null; }

            var firstObject = eflr.Objects.FirstOrDefault();
            if (firstObject == null) { return null; }
            if (firstObject.Attributes == null) { return null; }

            var seqenceNumberTemplate = eflr.Template.FirstOrDefault(x => string.Compare(x.Label, SEQUENCE_NUMBER_LABEL, true) == 0);
            if (seqenceNumberTemplate == null) { return null; }

            var sequenceNumberAttribute = firstObject.Attributes.FirstOrDefault(x => x.Template == seqenceNumberTemplate);
            if (sequenceNumberAttribute == null) return null;
            if (sequenceNumberAttribute.RepresentationCode != (byte)RepresentationCode.ASCII) { throw new Exception("invalid fhlr sequence number rep code"); }

            var idTemplate = eflr.Template.FirstOrDefault(x => string.Compare(x.Label, ID_LABEL, true) == 0);
            if (idTemplate == null) { return null; }

            var idAttribute = firstObject.Attributes.FirstOrDefault(x => x.Template == idTemplate);
            if (idAttribute == null) return null;
            if (idAttribute.RepresentationCode != (byte)RepresentationCode.ASCII) { throw new Exception("invalid fhlr id rep code"); }

            return new FileHeaderLogicalRecord
            {
                SequenceNumber = ((string)sequenceNumberAttribute.Value.First()).Trim(),
                ID = ((string)idAttribute.Value.First()).Trim()
            };
        }
    }
}
