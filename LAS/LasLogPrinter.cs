using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellLog.Lib.Models;

namespace LAS
{
    public class LasLogPrinter : ILasLogPrinter
    {
        private readonly TextWriter _textWriter;

        public LasLogPrinter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        private void DisplaySectionInfo(LasLog lasLog)
        {
            foreach (var section in lasLog.Sections)
            {
                _textWriter.WriteLine($"{section.SectionType}");

                if (section.MnemonicsLines != null)
                {
                    foreach (var mnemonicLine in section.MnemonicsLines)
                    {
                        _textWriter.WriteLine($"\tMnemonic: {mnemonicLine.Mnemonic}; Units: {mnemonicLine.Units}; Data: {mnemonicLine.Data}; Description: {mnemonicLine.Description}");
                    }
                }

                if (section.AsciiLogDataLines != null)
                {
                    foreach (var asciiLine in section.AsciiLogDataLines.Take(25))
                    {
                        _textWriter.WriteLine($"\t{string.Join('\t', asciiLine.Values)}");
                    }
                }
            }
        }

        private void DisplayValidationErrors(IEnumerable<ValidationError> validationErrors)
        {
            if (!validationErrors.Any())
            {
                _textWriter.WriteLine("No validation errors.");
                return;
            }

            foreach (var verr in validationErrors)
            {
                _textWriter.WriteLine(verr.Message);
            }
        }

        public void PrintLasLog(LasLog lasLog, IEnumerable<ValidationError> validationErrors)
        {
            if (lasLog == null) { throw new ArgumentNullException(nameof(lasLog)); }
            if (validationErrors == null) { throw new ArgumentNullException(nameof(validationErrors)); }

            DisplaySectionInfo(lasLog);
            _textWriter.WriteLine();
            _textWriter.WriteLine("----------");
            _textWriter.WriteLine();
            DisplayValidationErrors(validationErrors);

        }
    }
}
