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

        public void PrintLasLog(StorageSet storageSet)
        {
            if (storageSet == null) { throw new ArgumentNullException(nameof(storageSet)); }

            var storageUnit = storageSet.StorageUnits.First();
            _textWriter.WriteLine("Storage Unit Label");
            _textWriter.WriteLine($"StorageUnitSequenceNumber: {storageUnit.Label.StorageUnitSequenceNumber}");
            _textWriter.WriteLine($"DlisVersion: {storageUnit.Label.DlisVersion}");
            _textWriter.WriteLine($"StorageUnitStructure: {storageUnit.Label.StorageUnitStructure}");
            _textWriter.WriteLine($"MaximumRecordLength: {storageUnit.Label.MaximumRecordLength}");
            _textWriter.WriteLine($"StorageSetIdentifier: {storageUnit.Label.StorageSetIdentifier}");
            _textWriter.WriteLine();

            var segment = storageUnit.Segments.First();
            _textWriter.WriteLine("Storage Unit Segment Header");
            _textWriter.WriteLine($"LogicalRecordStructure: {segment.Header.LogicalRecordStructure}");
            _textWriter.WriteLine($"Predecessor: {segment.Header.Predecessor}");
            _textWriter.WriteLine($"Successor: {segment.Header.Successor}");
            _textWriter.WriteLine($"Encryption: {segment.Header.Encryption}");
            _textWriter.WriteLine($"EncryptionPacket: {segment.Header.EncryptionPacket}");
            _textWriter.WriteLine($"Checksum: {segment.Header.Checksum}");
            _textWriter.WriteLine($"TrailingLength: {segment.Header.TrailingLength}");
            _textWriter.WriteLine($"Padding: {segment.Header.Padding}");
        }
    }
}
