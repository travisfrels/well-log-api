using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellLog.Lib.Exceptions;

namespace WellLog.Lib.Helpers
{
    public static class LasStreamHelpers
    {
        private const byte ASCII_LINE_FEED = 10;
        private const byte ASCII_CARRIAGE_RETURN = 13;
        private const byte ASCII_SPACE = 32;

        public static byte ReadLasByte(this Stream lasStream)
        {
            if (lasStream == null) { return 0; }
            if (!lasStream.CanRead) { return 0; }

            var response = lasStream.ReadByte();
            if (response < 0) { return 0; }
            if (response == ASCII_LINE_FEED || response == ASCII_CARRIAGE_RETURN || (response >= 32 && response <= 126))
            {
                return Convert.ToByte(response);
            }

            return ASCII_SPACE;
        }

        public static string ReadLasLine(this Stream lasStream)
        {
            var response = new List<byte>();
            var endOfLine = false;
            var endOfFile = false;

            var b = lasStream.ReadLasByte();
            while (!endOfFile && !endOfLine)
            {
                if (b == 0) { endOfFile = true; }
                else if (b == ASCII_CARRIAGE_RETURN) { endOfLine = true; }
                else
                {
                    response.Add(b);
                    b = lasStream.ReadLasByte();
                }
            }

            if (endOfLine && lasStream.ReadLasByte() != ASCII_LINE_FEED)
            {
                throw new LasStreamException("Invalid end of line.");
            }

            if (response.Count == 0)
            {
                if (endOfFile) { return null; }
                return string.Empty;
            }

            return Encoding.ASCII.GetString(response.ToArray());
        }

        public static void SeekBackLine(this Stream lasStream, string lasLine)
        {
            /*
             * seek back to the beginning of the section header line so the section
             * reader can read the section header line.  add two bytes to the line
             * count to account for the carriage return and line feed
             */
            if (lasStream == null) { return; }
            if (!lasStream.CanSeek) { return; }
            if (string.IsNullOrEmpty(lasLine)) { return; }

            var seekLength = Math.Min(lasLine.Length + 2, lasStream.Position);
            lasStream.Seek(-seekLength, SeekOrigin.Current);
        }
    }
}
