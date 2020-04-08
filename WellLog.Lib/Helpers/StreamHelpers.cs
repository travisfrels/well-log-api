using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WellLog.Lib.Helpers
{
    public static class StreamHelpers
    {
        public static byte[] ReadBytes(this Stream s, int numBytes)
        {
            if (s == null) { return null; }
            if (numBytes < 0) { return null; }
            if (numBytes == 0) { return new byte[0]; }

            var buffer = new byte[numBytes];
            var bytesRead = s.Read(buffer, 0, numBytes);

            if (bytesRead < numBytes) { return null; }
            return buffer;
        }

        public static bool IsAtBeginningOfStream(this Stream s)
        {
            if (s == null) { return false; }
            return s.Position <= 0;
        }

        public static bool IsAtEndOfStream(this Stream s)
        {
            if (s == null) { return false; }
            return s.Position >= s.Length;
        }

        public static long BytesRemaining(this Stream s)
        {
            if (s == null) { return 0; }
            return s.Length - s.Position;
        }
    }
}
