using System;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class VisibleRecordBusiness : IVisibleRecordBusiness
    {
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

            visibleRecord.Data = dlisStream.ReadBytes(visibleRecord.Length - 4);
            if (visibleRecord.Data == null)
            {
                throw new Exception("invalid visible record length");
            }

            return visibleRecord;
        }
    }
}
