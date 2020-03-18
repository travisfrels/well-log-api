using NUnit.Framework;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasVersionInformationHelpersTests
    {
        private static readonly LasSection nullSection = null;
        private static readonly LasSection emptySection = new LasSection();
        private static readonly LasSection versionInformationSection = new LasSection
        {
            MnemonicsLines = new LasMnemonicLine[]
            {
                new LasMnemonicLine { Mnemonic = "VERS" },
                new LasMnemonicLine { Mnemonic = "WRAP" }
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
    }
}
