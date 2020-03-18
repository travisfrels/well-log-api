using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasMnemonicLineHelpersTests
    {
        private static readonly LasMnemonicLine nullMnemonicLine = null;
        private static readonly LasMnemonicLine mnemonicLine = new LasMnemonicLine { Mnemonic = "MNEM" };

        [Test]
        public void LasMnemonicLineHelpers_IsMnemonic_Pass()
        {
            Assert.IsFalse(nullMnemonicLine.IsMnemonic("MNEM"));
            Assert.IsTrue(mnemonicLine.IsMnemonic("MNEM"));
            Assert.IsTrue(mnemonicLine.IsMnemonic("A", "B", "MNEM"));
            Assert.IsFalse(mnemonicLine.IsMnemonic("A"));
        }
    }
}
