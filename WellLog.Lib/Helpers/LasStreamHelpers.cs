using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WellLog.Lib.Exceptions;

namespace WellLog.Lib.Helpers
{
    public static class LasStreamHelpers
    {
        private const byte ASCII_LINE_FEED = 10;
        private const byte ASCII_CARRIAGE_RETURN = 13;
        private const byte ASCII_SPACE = 32;

        public static byte ToLasByte(this byte b)
        {
            if (b == ASCII_LINE_FEED || b == ASCII_CARRIAGE_RETURN || (b >= 32 && b <= 126))
            {
                return b;
            }

            return ASCII_SPACE;
        }

        public static byte ReadLasByte(this Stream lasStream)
        {
            if (lasStream == null) { return 0; }
            if (!lasStream.CanRead) { return 0; }

            var b = lasStream.ReadByte();
            if (b < 0) { return 0; }

            return Convert.ToByte(b).ToLasByte();
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

        public static void WriteLasLine(this Stream lasStream, string s)
        {
            if (lasStream == null) { return; }
            if (!lasStream.CanWrite) { return; }

            if (string.IsNullOrWhiteSpace(s))
            {
                lasStream.Write(new byte[2] { ASCII_CARRIAGE_RETURN, ASCII_LINE_FEED }, 0, 2);
            }
            else
            {
                var bytes = Encoding.ASCII.GetBytes($"{s}\r\n").Select(x => x.ToLasByte()).ToArray();
                lasStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
