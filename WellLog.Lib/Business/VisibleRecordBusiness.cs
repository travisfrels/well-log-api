using System;
using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class VisibleRecordBusiness : IVisibleRecordBusiness
    {
        private readonly ILogicalRecordSegmentBusiness _logicalRecordSegmentBusiness;

        public VisibleRecordBusiness(ILogicalRecordSegmentBusiness logicalRecordSegmentBusiness)
        {
            _logicalRecordSegmentBusiness = logicalRecordSegmentBusiness;
        }

        public VisibleRecord ReadVisibleRecord(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var visibleRecord = new VisibleRecord
            {
                Length = dlisStream.ReadUNORM(),
                FormatVersionField1 = dlisStream.ReadUSHORT(),
                FormatVersionField2 = dlisStream.ReadUSHORT()
            };

            if (visibleRecord.FormatVersionField1 != 255 || visibleRecord.FormatVersionField2 != 1)
            {
                throw new Exception("invalid visible record format");
            }

            var logicalRecordSegmentData = dlisStream.ReadBytes(visibleRecord.Length - 4);
            if (logicalRecordSegmentData == null) { throw new Exception("invalid visible record length"); }

            var logicalRecordSegments = new List<LogicalRecordSegment>();
            using (var lrsStream = new MemoryStream(logicalRecordSegmentData))
            {
                while (!lrsStream.IsAtEndOfStream())
                {
                    var lrs = _logicalRecordSegmentBusiness.ReadLogicalRecordSegment(lrsStream);
                    if (lrs == null) { throw new Exception("invalid logical record segment"); }
                    logicalRecordSegments.Add(lrs);
                }
            }
            visibleRecord.Segments = logicalRecordSegments.ToArray();

            return visibleRecord;
        }
    }
}
