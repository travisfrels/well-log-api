using System;
using System.IO;
using System.Linq;
using WellLog.Lib.Helpers;
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

            foreach(var origin in dlisLog.Origins)
            {
                _textWriter.WriteLine("Origin");
                _textWriter.WriteLine($"\tFile ID            : {origin.FileID}");
                _textWriter.WriteLine($"\tFile Set Name      : {origin.FileSetName}");
                _textWriter.WriteLine($"\tFile Set Number    : {origin.FileSetNumber}");
                _textWriter.WriteLine($"\tFile Number        : {origin.FileNumber}");
                _textWriter.WriteLine($"\tFile Type          : {origin.FileType}");
                _textWriter.WriteLine($"\tProduct            : {origin.Product}");
                _textWriter.WriteLine($"\tVersion            : {origin.Version}");
                _textWriter.WriteLine($"\tPrograms           : {origin.Programs}");
                _textWriter.WriteLine($"\tCreation Time      : {origin.CreationTime}");
                _textWriter.WriteLine($"\tOrder Number       : {origin.OrderNumber}");
                _textWriter.WriteLine($"\tDescent Number     : {origin.DescentNumber}");
                _textWriter.WriteLine($"\tRun Number         : {origin.RunNumber}");
                _textWriter.WriteLine($"\tWell Name          : {origin.WellName}");
                _textWriter.WriteLine($"\tField Name         : {origin.FieldName}");
                _textWriter.WriteLine($"\tProducer Code      : {origin.ProducerCode}");
                _textWriter.WriteLine($"\tProducer Name      : {origin.ProducerName}");
                _textWriter.WriteLine($"\tCompany            : {origin.Company}");
                _textWriter.WriteLine($"\tName Space Name    : {origin.NameSpaceName}");
                _textWriter.WriteLine($"\tName Space Version : {origin.NameSpaceVersion}");
                _textWriter.WriteLine();
            }

            foreach (var param in dlisLog.Parameters)
            {
                _textWriter.WriteLine("Parameter");
                _textWriter.WriteLine($"\tLong Name : {param.LongName}");
                _textWriter.WriteLine($"\tDimension : {param.Dimension}");
                _textWriter.WriteLine($"\tAxis      : {{ Origin: {param.Axis?.Origin}; CopyNumber: {param.Axis?.CopyNumber}; Identifier: {param.Axis?.Identifier} }}");
                _textWriter.WriteLine($"\tZones     : {{ Origin: {param.Zones?.Origin}; CopyNumber: {param.Zones?.CopyNumber}; Identifier: {param.Zones?.Identifier} }}");
                _textWriter.WriteLine($"\tValues    : {string.Join(", ", param.Values.SerializeValues().ToArray())}");
                _textWriter.WriteLine();
            }
        }
    }
}
