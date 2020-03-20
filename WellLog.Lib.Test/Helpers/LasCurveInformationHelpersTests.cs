using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasCurveInformationHelpersTests
    {
        private const string MNEM_DEPT = "DEPT";
        private const string MNEM_DEPTH = "DEPTH";
        private const string MNEM_TIME = "TIME";
        private const string MNEM_INDEX = "INDEX";

        [Test]
        public void LasCurveInformationHelpers_HasDepthMnemonic_Pass_HasDept()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_DEPT } } };
            Assert.IsTrue(lasSection.HasDepthMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasDepthMnemonic_Pass_HasDepth()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_DEPTH } } };
            Assert.IsTrue(lasSection.HasDepthMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasDepthMnemonic_Pass_NoDepth()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "MNEM" } } };
            Assert.IsFalse(lasSection.HasDepthMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasTimeMnemonic_Pass_HasTime()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_TIME } } };
            Assert.IsTrue(lasSection.HasTimeMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasTimeMnemonic_Pass_NoTime()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "MNEM" } } };
            Assert.IsFalse(lasSection.HasTimeMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexMnemonic_Pass_HasIndex()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_INDEX } } };
            Assert.IsTrue(lasSection.HasIndexMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexMnemonic_Pass_NoIndex()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "MNEM" } } };
            Assert.IsFalse(lasSection.HasIndexMnemonic());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexChannel_Pass_HasDept()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_DEPT } } };
            Assert.IsTrue(lasSection.HasIndexChannel());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexChannel_Pass_HasDepth()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_DEPTH } } };
            Assert.IsTrue(lasSection.HasIndexChannel());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexChannel_Pass_HasTime()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_TIME } } };
            Assert.IsTrue(lasSection.HasIndexChannel());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexChannel_Pass_HasIndex()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = MNEM_INDEX } } };
            Assert.IsTrue(lasSection.HasIndexChannel());
        }

        [Test]
        public void LasCurveInformationHelpers_HasIndexChannel_Pass_NoIndexChannel()
        {
            var lasSection = new LasSection { MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "MNEM" } } };
            Assert.IsFalse(lasSection.HasIndexChannel());
        }
    }
}
