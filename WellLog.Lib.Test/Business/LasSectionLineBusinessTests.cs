using NUnit.Framework;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LasSectionLineBusinessTests
    {
        private LasSectionLineBusiness _lasSectionLineBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _lasSectionLineBusiness = new LasSectionLineBusiness();
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Fail_NotSectionHeader()
        {
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToSectionTypeFromLasLine("NOT HEADER"); });
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Fail_InvalidSectionHeader()
        {
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToSectionTypeFromLasLine("~INVALID HEADER"); });
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Pass_VersionInformation()
        {
            Assert.AreEqual(LasSectionType.VersionInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~V"));
            Assert.AreEqual(LasSectionType.VersionInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~VERSION INFORMATION"));
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Pass_WellInformation()
        {
            Assert.AreEqual(LasSectionType.WellInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~W"));
            Assert.AreEqual(LasSectionType.WellInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~WELL INFORMATION"));
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Pass_CurveInformation()
        {
            Assert.AreEqual(LasSectionType.CurveInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~C"));
            Assert.AreEqual(LasSectionType.CurveInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~CURVE INFORMATION"));
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Pass_ParameterInformation()
        {
            Assert.AreEqual(LasSectionType.ParameterInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~P"));
            Assert.AreEqual(LasSectionType.ParameterInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~PARAMETER INFORMATION"));
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Pass_OtherInformation()
        {
            Assert.AreEqual(LasSectionType.OtherInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~O"));
            Assert.AreEqual(LasSectionType.OtherInformation, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~OTHER INFORMATION"));
        }

        [Test]
        public void LasSectionLineBusiness_ToSectionTypeFromLasLine_Pass_AsciiLogData()
        {
            Assert.AreEqual(LasSectionType.AsciiLogData, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~A"));
            Assert.AreEqual(LasSectionType.AsciiLogData, _lasSectionLineBusiness.ToSectionTypeFromLasLine("~ASCII LOG DATA"));
        }

        [Test]
        public void LasSectionLineBusiness_ToMnemonicLineFromLasLine_Pass_EmbeddedBlankLine()
        {
            Assert.IsNull(_lasSectionLineBusiness.ToMnemonicLineFromLasLine(null));
            Assert.IsNull(_lasSectionLineBusiness.ToMnemonicLineFromLasLine(string.Empty));
            Assert.IsNull(_lasSectionLineBusiness.ToMnemonicLineFromLasLine(" \t "));
        }

        [Test]
        public void LasSectionLineBusiness_ToMnemonicLineFromLasLine_Pass_Comment()
        {
            Assert.IsNull(_lasSectionLineBusiness.ToMnemonicLineFromLasLine("#COMMENT"));
        }

        [Test]
        public void LasSectionLineBusiness_ToMnemonicLineFromLasLine_Fail_SectionHeader()
        {
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToMnemonicLineFromLasLine("~SECTION HEADER"); });
        }

        [Test]
        public void LasSectionLineBusiness_ToMnemonicLineFromLasLine_Fail_NotDelimited()
        {
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToMnemonicLineFromLasLine("NOT DELIMITED"); });
        }

        [Test]
        public void LasSectionLineBusiness_ToMnemonicLineFromLasLine_Pass()
        {
            var mnemonicLine = _lasSectionLineBusiness.ToMnemonicLineFromLasLine("MNEM.UNIT DATA:DESCRIPTION");

            Assert.AreEqual("MNEM", mnemonicLine.Mnemonic);
            Assert.AreEqual("UNIT", mnemonicLine.Units);
            Assert.AreEqual("DATA", mnemonicLine.Data);
            Assert.AreEqual("DESCRIPTION", mnemonicLine.Description);

            mnemonicLine = _lasSectionLineBusiness.ToMnemonicLineFromLasLine(" MNEM.UNIT  D A T A : D E S C R I P T I O N ");

            Assert.AreEqual("MNEM", mnemonicLine.Mnemonic);
            Assert.AreEqual("UNIT", mnemonicLine.Units);
            Assert.AreEqual("D A T A", mnemonicLine.Data);
            Assert.AreEqual("D E S C R I P T I O N", mnemonicLine.Description);
        }

        [Test]
        public void LasSectionLineBusiness_ToAsciiLogDataLineFromLasLine_Fail_InvalidLine()
        {
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine(null); });
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine(string.Empty); });
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine(" \t "); });
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine("#COMMENT"); });
            Assert.Throws<LasLineException>(() => { _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine("~SECTION HEADER"); });
        }

        [Test]
        public void LasSectionLineBusiness_ToAsciiLogDataLineFromLasLine_Pass()
        {
            Assert.AreEqual(1, _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine("100").Values.Count());
            Assert.AreEqual(3, _lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine("100 100 100").Values.Count());
        }
    }
}
