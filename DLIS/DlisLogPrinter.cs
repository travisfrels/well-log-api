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

            _textWriter.WriteLine($"Label");
            _textWriter.WriteLine($"\tStorage Unit Sequence Number : {dlisLog.Label.StorageUnitSequenceNumber}");
            _textWriter.WriteLine($"\tDLIS Version                 : {dlisLog.Label.DlisVersion}");
            _textWriter.WriteLine($"\tStorage Unit Structure       : {dlisLog.Label.StorageUnitStructure}");
            _textWriter.WriteLine($"\tMaximum Record Length        : {dlisLog.Label.MaximumRecordLength}");
            _textWriter.WriteLine($"\tStorage Set Identifier       : {dlisLog.Label.StorageSetIdentifier}");
            _textWriter.WriteLine();

            _textWriter.WriteLine($"EFLRs Found: {dlisLog.EFLRs.Count()}");
            foreach (var eflr in dlisLog.EFLRs)
            {
                _textWriter.WriteLine($"\tSet: {{ Name: {eflr.Set.Name}, Type: {eflr.Set.Type} }}");
            }
            _textWriter.WriteLine();

            _textWriter.WriteLine("File Header");
            _textWriter.WriteLine($"\tSequence Number : {dlisLog.FileHeader.SequenceNumber}");
            _textWriter.WriteLine($"\tID              : {dlisLog.FileHeader.ID}");
            _textWriter.WriteLine();
        }
    }
}
