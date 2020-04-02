using System;
using System.IO;

namespace WellLog.Lib.Helpers
{
    public static class DlisStreamHelpers
    {
        public static uint ReadUNORM(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            var buffer = new byte[2];
            dlisStream.Read(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer);
        }
    }
}
