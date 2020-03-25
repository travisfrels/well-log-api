using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasVersionInformationHelpersTests
    {
        private static readonly LasMnemonicLine versionMnemonicLine = new LasMnemonicLine { Mnemonic = "VERS", Data = "2.0", Description = "LAS FILE VERSION" };
        private static readonly LasMnemonicLine wrapMnemonicLine = new LasMnemonicLine { Mnemonic = "WRAP", Data = "NO", Description = "USES LINE WRAP" };

        private static readonly LasSection nullSection = null;
        private static readonly LasSection emptySection = new LasSection();
        private static readonly LasSection versionInformationSection = new LasSection
        {
            MnemonicsLines = new LasMnemonicLine[]
            {
                versionMnemonicLine,
                wrapMnemonicLine
            }
        };

        [Test]
        public void LasVersionInformationHelpers_HasVersionMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasVersionMnemonic());
            Assert.IsFalse(emptySection.HasVersionMnemonic());
            Assert.IsTrue(versionInformationSection.HasVersionMnemonic());
        }

        [Test]
        public void LasVersionInformationHelpers_HasWrapMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasWrapMnemonic());
            Assert.IsFalse(emptySection.HasWrapMnemonic());
            Assert.IsTrue(versionInformationSection.HasWrapMnemonic());
        }

        [Test]
        public void LasVersionInformationHelpers_GetVersionMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetVersionMnemonic());
            Assert.IsNull(emptySection.GetVersionMnemonic());
            Assert.AreSame(versionMnemonicLine, versionInformationSection.GetVersionMnemonic());
        }
    }
}
