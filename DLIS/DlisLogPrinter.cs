using System;
using System.IO;
using System.Linq;
using WellLog.Lib.Models.DLIS;

namespace DLIS
{
    public class DlisLogPrinter : IDlisLogPrinter
    {
        private readonly TextWriter _textWriter;

        public DlisLogPrinter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void PrintLasLog(DlisLog dlisLog)
        {
            if (dlisLog == null) { throw new ArgumentNullException(nameof(dlisLog)); }

            _textWriter.WriteLine($"EFLRs Found: {dlisLog.EFLRs.Count()}");
            foreach (var eflr in dlisLog.EFLRs)
            {
                _textWriter.WriteLine($"\tSet: {{ Name: {eflr.Set.Name}, Type: {eflr.Set.Type} }}");
            }
        }
    }
}
