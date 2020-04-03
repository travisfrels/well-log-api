using NUnit.Framework;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class ByteHelpersTests
    {
        [Test]
        public void ByteHelpers_GetBitUsingMask_Pass_HasMaskBit()
        {
            byte b = 0b_1000_0000;
            byte mask = 0b_1000_0000;
            Assert.IsTrue(b.GetBitUsingMask(mask));
        }

        [Test]
        public void ByteHelpers_GetBitUsingMask_Pass_NoMaskBit()
        {
            byte b = 0b_0000_0000;
            byte mask = 0b_1000_0000;
            Assert.IsFalse(b.GetBitUsingMask(mask));
        }

        [Test]
        public void ByteHelpers_AssignBitUsingMask_Pass_AssignBitTrue()
        {
            byte b = 0b_0000_0000;
            byte mask = 0b_0000_0001;
            byte expected = 0b_0000_0001;
            Assert.AreEqual(expected, b.AssignBitUsingMask(mask, true));
        }

        [Test]
        public void ByteHelpers_AssignBitUsingMask_Pass_AssignBitFalse()
        {
            byte b = 0b_0000_0001;
            byte mask = 0b_0000_0001;
            byte expected = 0b_0000_0000;
            Assert.AreEqual(expected, b.AssignBitUsingMask(mask, false));
        }

        [Test]
        public void ByteHelpers_IsComponentRole_Pass_True()
        {
            byte attributeDescriptor = 0b_0011_1111;
            byte attributeRole = 0b_0010_0000;
            Assert.IsTrue(attributeDescriptor.IsComponentRole(attributeRole));
        }

        [Test]
        public void ByteHelpers_IsComponentRole_Pass_False()
        {
            byte setDescriptor = 0b_1111_1000;
            byte attributeRole = 0b_0010_0000;
            Assert.IsFalse(setDescriptor.IsComponentRole(attributeRole));
        }

        [Test]
        public void ByteHelpers_ShiftLeftByte_Pass()
        {
            byte b = 0b_0000_0001;
            byte expected = 0b_0000_0100;
            Assert.AreEqual(expected, b.ShiftLeft(2));
        }

        [Test]
        public void ByteHelpers_ShiftRightByte_Pass()
        {
            byte b = 0b_1000_0000;
            byte expected = 0b_0010_0000;
            Assert.AreEqual(expected, b.ShiftRight(2));
        }

        [Test]
        public void ByteHelpers_ShiftLeftArray_Pass_NullBytes()
        {
            byte[] bytes = null;
            Assert.IsNull(bytes.ShiftLeft());
        }

        [Test]
        public void ByteHelpers_ShiftLeftArray_Pass_ZeroBytes()
        {
            var bytes = new byte[0];
            Assert.AreEqual(bytes, bytes.ShiftLeft());
        }

        [Test]
        public void ByteHelpers_ShiftLeftArray_Pass()
        {
            var bytes = new byte[] { 0b_1000_0001, 0b_1000_0001 };
            var expected = new byte[] { 0b_0000_0011, 0b_0000_0010 };
            Assert.AreEqual(expected, bytes.ShiftLeft());
        }

        [Test]
        public void ByteHelpers_ShiftRightArray_Pass_NullBytes()
        {
            byte[] bytes = null;
            Assert.IsNull(bytes.ShiftRight());
        }

        [Test]
        public void ByteHelpers_ShiftRightArray_Pass_ZeroBytes()
        {
            var bytes = new byte[0];
            Assert.AreEqual(bytes, bytes.ShiftRight());
        }

        [Test]
        public void ByteHelpers_ShiftRightArray_Pass()
        {
            var bytes = new byte[] { 0b_1000_0001, 0b_1000_0001 };
            var expected = new byte[] { 0b_0100_0000, 0b_1100_0000 };
            Assert.AreEqual(expected, bytes.ShiftRight());
        }
    }
}
