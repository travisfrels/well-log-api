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

        public void PrintLasLog(StorageUnit storageUnit)
        {
            if (storageUnit == null) { throw new ArgumentNullException(nameof(storageUnit)); }

            _textWriter.WriteLine("Storage Unit Label");
            _textWriter.WriteLine($"StorageUnitSequenceNumber: {storageUnit.Label.StorageUnitSequenceNumber}");
            _textWriter.WriteLine($"DlisVersion: {storageUnit.Label.DlisVersion}");
            _textWriter.WriteLine($"StorageUnitStructure: {storageUnit.Label.StorageUnitStructure}");
            _textWriter.WriteLine($"MaximumRecordLength: {storageUnit.Label.MaximumRecordLength}");
            _textWriter.WriteLine($"StorageSetIdentifier: {storageUnit.Label.StorageSetIdentifier}");
            _textWriter.WriteLine();
            _textWriter.WriteLine($"Visible Records: {storageUnit.VisibleRecords.Count()}");
            _textWriter.WriteLine($"Logical Record Segments: {storageUnit.VisibleRecords.SelectMany(x => x.Segments).Count()}");
        }
    }
}
