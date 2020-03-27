using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text;
using WellLog.Lib.Business;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LasSectionBusinessTests
    {
        private Mock<ILasSectionLineBusiness> _lasSectionLineBusiness;
        private LasSectionBusiness _lasSectionBusiness;

        private Stream StreamFromLasLine(string lasLine)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(lasLine));
        }

        [SetUp]
        public void PerTestSetup()
        {
            _lasSectionLineBusiness = new Mock<ILasSectionLineBusiness>();
            _lasSectionBusiness = new LasSectionBusiness(_lasSectionLineBusiness.Object);
        }

        [Test]
        public void LasSectionBusiness_ReadMnemonicSection_Pass_NullStream()
        {
            Assert.IsNull(_lasSectionBusiness.ReadMnemonicSection(null));
        }

        [Test]
        public void LasSectionBusiness_ReadMnemonicSection_Pass_ReadToEndOfStream()
        {
            var mnemonicSection = "MNE1.UNIT DATA:DESCRIPTION\r\nMNE2.UNIT DATA:DESCRIPTION\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadMnemonicSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadMnemonicSection_Pass_ReadToEndOfSection()
        {
            var mnemonicSection = "MNE1.UNIT DATA:DESCRIPTION\r\nMNE2.UNIT DATA:DESCRIPTION\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadMnemonicSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadMnemonicSection_Pass_IgnoreComments()
        {
            var mnemonicSection = "MNE1.UNIT DATA:DESCRIPTION\r\n#COMMENT\r\nMNE2.UNIT DATA:DESCRIPTION\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadMnemonicSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadMnemonicSection_Pass_IgnoreBlankLines()
        {
            var mnemonicSection = "MNE1.UNIT DATA:DESCRIPTION\r\n \t \r\nMNE2.UNIT DATA:DESCRIPTION\r\n\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadMnemonicSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadAsciiLogDataSection_Pass_NullStream()
        {
            Assert.IsNull(_lasSectionBusiness.ReadAsciiLogDataSection(null));
        }

        [Test]
        public void LasSectionBusiness_ReadAsciiLogDataSection_Pass_ReadToEndOfStream()
        {
            var asciiSection = "-999.25 -999.25 -999.25\r\n-999.25 -999.25 -999.25\r\n";
            var lasStream = StreamFromLasLine(asciiSection);
            var mnemonicLines = _lasSectionBusiness.ReadAsciiLogDataSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadAsciiLogDataSection_Pass_ReadToEndOfSection()
        {
            var asciiSection = "-999.25 -999.25 -999.25\r\n-999.25 -999.25 -999.25\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(asciiSection);
            var mnemonicLines = _lasSectionBusiness.ReadAsciiLogDataSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadAsciiLogDataSection_Fail_Comments()
        {
            var asciiSection = "-999.25 -999.25 -999.25\r\n#COMMENT\r\n-999.25 -999.25 -999.25\r\n";
            var lasStream = StreamFromLasLine(asciiSection);
            Assert.Throws<LasLineException>(() => { _lasSectionBusiness.ReadAsciiLogDataSection(lasStream); });
        }

        [Test]
        public void LasSectionBusiness_ReadAsciiLogDataSection_Fail_EmbeddedBlankLine()
        {
            var asciiSection = "-999.25 -999.25 -999.25\r\n\r\n-999.25 -999.25 -999.25\r\n";
            var lasStream = StreamFromLasLine(asciiSection);
            Assert.Throws<LasLineException>(() => { _lasSectionBusiness.ReadAsciiLogDataSection(lasStream); });
        }

        [Test]
        public void LasSectionBusiness_ReadAsciiLogDataSection_Fail_EmbeddedWhiteSpaceLine()
        {
            var asciiSection = "-999.25 -999.25 -999.25\r\n \t \r\n-999.25 -999.25 -999.25\r\n";
            var lasStream = StreamFromLasLine(asciiSection);
            Assert.Throws<LasLineException>(() => { _lasSectionBusiness.ReadAsciiLogDataSection(lasStream); });
        }

        [Test]
        public void LasSectionBusiness_ReadOtherSection_Pass_NullStream()
        {
            Assert.IsNull(_lasSectionBusiness.ReadOtherSection(null));
        }

        [Test]
        public void LasSectionBusiness_ReadOtherSection_Pass_ReadToEndOfStream()
        {
            var mnemonicSection = "BLAH BLAH BLAH\r\nBLAH BLAH BLAH\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadOtherSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadOtherSection_Pass_ReadToEndOfSection()
        {
            var mnemonicSection = "BLAH BLAH BLAH\r\nBLAH BLAH BLAH\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadOtherSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadOtherSection_Pass_IgnoreComments()
        {
            var mnemonicSection = "BLAH BLAH BLAH\r\n#COMMENT\r\nBLAH BLAH BLAH\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadOtherSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadOtherSection_Pass_IgnoreBlankLines()
        {
            var mnemonicSection = "BLAH BLAH BLAH\r\n \t \r\nBLAH BLAH BLAH\r\n\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(mnemonicSection);
            var mnemonicLines = _lasSectionBusiness.ReadOtherSection(lasStream);

            Assert.AreEqual(2, mnemonicLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_NullStream()
        {
            Assert.IsNull(_lasSectionBusiness.ReadSection(null));
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_VersionInformationSection()
        {
            var versionInformation = "~VERSION INFORMATION\r\n \t \r\nVERS. 2.0:VERSION\r\nWRAP. NO:LINE WRAP\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(versionInformation);
            _lasSectionLineBusiness.Setup(x => x.ToSectionTypeFromLasLine(versionInformation)).Returns(LasSectionType.VersionInformation);

            var versionInformationSection = _lasSectionBusiness.ReadSection(lasStream);

            Assert.AreEqual(2, versionInformationSection.MnemonicsLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_WellInformationSection()
        {
            var wellInformation = "~WELL INFORMATION\r\n \t \r\nSTRT.FEET 100.0:START\r\nSTOP.FEET 200:STOP\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(wellInformation);
            _lasSectionLineBusiness.Setup(x => x.ToSectionTypeFromLasLine(wellInformation)).Returns(LasSectionType.WellInformation);

            var wellInformationSection = _lasSectionBusiness.ReadSection(lasStream);

            Assert.AreEqual(2, wellInformationSection.MnemonicsLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_CurveInformationSection()
        {
            var curveInformation = "~CURVE INFORMATION\r\n \t \r\nDEPT.FEET 00 001 00 00:MEASURED DEPTH\r\nGR.GAPI 07 310 01 00:Gamma Ray\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(curveInformation);
            _lasSectionLineBusiness.Setup(x => x.ToSectionTypeFromLasLine(curveInformation)).Returns(LasSectionType.CurveInformation);

            var curveInformationSection = _lasSectionBusiness.ReadSection(lasStream);

            Assert.AreEqual(2, curveInformationSection.MnemonicsLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_ParameterInformationSection()
        {
            var parameterInformation = "~PARAMETER INFORMATION\r\n \t \r\nPRM1. VALUE1:PARAMETER 1\r\nPRM2. VALUE2:PARAMETER 2\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(parameterInformation);
            _lasSectionLineBusiness.Setup(x => x.ToSectionTypeFromLasLine(parameterInformation)).Returns(LasSectionType.ParameterInformation);

            var parameterInformationSection = _lasSectionBusiness.ReadSection(lasStream);

            Assert.AreEqual(2, parameterInformationSection.MnemonicsLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_OtherInformationSection()
        {
            var otherInformation = "~OTHER INFORMATION\r\n \t \r\nBLAH BLAH BLAH\r\nBLAH BLAH BLAH\r\n~SECTION HEADER\r\n";
            var lasStream = StreamFromLasLine(otherInformation);
            _lasSectionLineBusiness.Setup(x => x.ToSectionTypeFromLasLine(otherInformation)).Returns(LasSectionType.OtherInformation);

            var otherInformationSection = _lasSectionBusiness.ReadSection(lasStream);

            Assert.AreEqual(2, otherInformationSection.MnemonicsLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Pass_AsciiLogDataSection()
        {
            var asciiLogData = "~ASCII LOG DATA\r\n \t \r\n-999.25 -999.25 -999.25\r\n-999.25 -999.25 -999.25\r\n";
            var lasStream = StreamFromLasLine(asciiLogData);
            _lasSectionLineBusiness.Setup(x => x.ToSectionTypeFromLasLine(asciiLogData)).Returns(LasSectionType.AsciiLogData);

            var asciiLogDataSection = _lasSectionBusiness.ReadSection(lasStream);

            Assert.AreEqual(2, asciiLogDataSection.MnemonicsLines.Count());
        }

        [Test]
        public void LasSectionBusiness_ReadSection_Fail_NoSectionHeader()
        {
            var sectionInformation = "BLAH BLAH BLAH\r\n";
            var lasStream = StreamFromLasLine(sectionInformation);
            Assert.Throws<LasStreamException>(() => { _lasSectionBusiness.ReadSection(lasStream); });
        }
    }
}
