using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasAsciiLogDataLineHelpersTests
    {
        private static readonly LasAsciiLogDataLine nullAsciiLogDataLine = null;
        private static readonly LasAsciiLogDataLine emptyAsciiLogDataLine = new LasAsciiLogDataLine();
        private static readonly LasAsciiLogDataLine noValuesAsciiLogDataLine = new LasAsciiLogDataLine { Values = new string[0] };
        private static readonly LasAsciiLogDataLine asciiLogDataLine = new LasAsciiLogDataLine { Values = new string[] { "100.0", "-999.25", "-999.25" } };

        [Test]
        public void LasMnemonicLineHelpers_IsEmpty_Pass()
        {
            Assert.IsTrue(nullAsciiLogDataLine.IsEmpty());
            Assert.IsTrue(emptyAsciiLogDataLine.IsEmpty());
            Assert.IsTrue(noValuesAsciiLogDataLine.IsEmpty());
            Assert.IsFalse(asciiLogDataLine.IsEmpty());
        }
    }
}
