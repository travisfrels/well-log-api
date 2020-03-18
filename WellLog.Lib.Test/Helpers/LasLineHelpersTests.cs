using NUnit.Framework;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasLineHelpersTests
    {
        [Test]
        public void LasLineHelpers_IsLasComment_Pass_IsComment()
        {
            Assert.IsTrue("#COMMENT".IsLasComment());
            Assert.IsTrue("   #COMMENT".IsLasComment());
            Assert.IsTrue("\t#COMMENT".IsLasComment());
        }

        [Test]
        public void LasLineHelpers_IsLasComment_Pass_IsNotComment()
        {
            Assert.IsFalse("NOT COMMENT".IsLasComment());
        }

        [Test]
        public void LasLineHelpers_IsLasSectionHeader_Pass_IsLasSectionHeader()
        {
            Assert.IsTrue("~SECTION HEADER".IsLasSectionHeader());
            Assert.IsTrue("   ~SECTION HEADER".IsLasSectionHeader());
            Assert.IsTrue("\t~SECTION HEADER".IsLasSectionHeader());
        }

        [Test]
        public void LasLineHelpers_IsLasSectionHeader_Pass_IsNotSectionHeader()
        {
            Assert.IsFalse("NOT SECTION HEADER".IsLasSectionHeader());
        }
    }
}
