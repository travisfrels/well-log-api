using System;
using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Factories.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class VisibleRecordBusiness : IVisibleRecordBusiness
    {
        private readonly IUNORMReader _unormReader;
        private readonly IUSHORTReader _ushortReader;
        private readonly ILogicalRecordSegmentBusiness _logicalRecordSegmentBusiness;

        public VisibleRecordBusiness(IUNORMReader unormReader, IUSHORTReader ushortReader, ILogicalRecordSegmentBusiness logicalRecordSegmentBusiness)
        {
            _unormReader = unormReader;
            _ushortReader = ushortReader;
            _logicalRecordSegmentBusiness = logicalRecordSegmentBusiness;
        }

        public VisibleRecord ReadVisibleRecord(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var visibleRecord = new VisibleRecord
            {
                Length = _unormReader.ReadUNORM(dlisStream),
                FormatVersionField1 = _ushortReader.ReadUSHORT(dlisStream),
                FormatVersionField2 = _ushortReader.ReadUSHORT(dlisStream)
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
