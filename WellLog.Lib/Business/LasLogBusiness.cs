using System;
using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public class LasLogBusiness : ILasLogBusiness
    {
        private readonly ILasSectionBusiness _lasSectionBusiness;
        private readonly IAsciiLogDataBusiness _asciiLogDataBusiness;
        private readonly IWellInformationBusiness _wellInformationBusiness;

        public LasLogBusiness(ILasSectionBusiness lasSectionBusiness, IAsciiLogDataBusiness asciiLogDataBusiness, IWellInformationBusiness wellInformationBusiness)
        {
            _lasSectionBusiness = lasSectionBusiness;
            _asciiLogDataBusiness = asciiLogDataBusiness;
            _wellInformationBusiness = wellInformationBusiness;
        }

        public LasLog ReadStream(Stream lasStream)
        {
            if (lasStream == null) { throw new ArgumentNullException(nameof(lasStream)); }

            var lasLog = new LasLog();
            var sections = new List<LasSection>();

            lasStream.Seek(0, SeekOrigin.Begin);
            var lasLine = lasStream.ReadLasLine();
            while (!string.IsNullOrEmpty(lasLine))
            {
                if (lasLine.IsLasSectionHeader())
                {
                    lasStream.SeekBackLine(lasLine);
                    sections.Add(_lasSectionBusiness.ReadSection(lasStream));
                }

                lasLine = lasStream.ReadLasLine();
            }
            lasLog.Sections = sections.ToArray();

            if (lasLog.UsesLineWrap)
            {
                _asciiLogDataBusiness.UnWrapAsciiLogData(lasLog.AsciiLogData);
            }

            if (lasLog.FileVersion == LasFileVersion.LAS_1_2)
            {
                _wellInformationBusiness.FixWellInformation(lasLog.WellInformation);
            }

            return lasLog;
        }

        public void WriteStream(Stream lasStream, LasLog lasLog)
        {
            if (lasStream == null) { throw new ArgumentNullException(nameof(lasStream)); }

            if (lasLog == null) { return; }
            foreach(var section in lasLog.Sections)
            {
                _lasSectionBusiness.WriteSection(lasStream, section);
            }
        }
    }
}
