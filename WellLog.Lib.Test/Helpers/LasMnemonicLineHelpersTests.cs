using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasMnemonicLineHelpersTests
    {
        private static readonly LasMnemonicLine nullMnemonicLine = null;
        private static readonly LasMnemonicLine mnemonicLine = new LasMnemonicLine { Mnemonic = "MNEM", Data = "DATA", Description = "DESCRIPTION" };

        [Test]
        public void LasMnemonicLineHelpers_IsMnemonic_Pass()
        {
            Assert.IsFalse(nullMnemonicLine.IsMnemonic("MNEM"));
            Assert.IsTrue(mnemonicLine.IsMnemonic("MNEM"));
            Assert.IsTrue(mnemonicLine.IsMnemonic("A", "B", "MNEM"));
            Assert.IsFalse(mnemonicLine.IsMnemonic("A"));
        }

        [Test]
        public void LasMnemonicLineHelpers_SwapDataDescription_Pass()
        {
            var data = "DATA";
            var description = "DESCRIPTION";
            var dataMnemonicLine = new LasMnemonicLine { Data = data, Description = description };
            dataMnemonicLine.SwapDataDescription();

            Assert.DoesNotThrow(() => nullMnemonicLine.SwapDataDescription());
            Assert.AreEqual(data, dataMnemonicLine.Description);
            Assert.AreEqual(description, dataMnemonicLine.Data);
        }
    }
}
