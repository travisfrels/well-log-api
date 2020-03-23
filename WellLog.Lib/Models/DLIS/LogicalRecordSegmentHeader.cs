using System;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentHeader
    {
        private const byte LOGICAL_RECORD_STRUCTURE_FLAG = 0b_1000_0000;
        private const byte PREDECESSOR_FLAG = 0b_0100_0000;
        private const byte SUCCESSOR_FLAG = 0b_0010_0000;
        private const byte ENCRYPTION_FLAG = 0b_0001_0000;
        private const byte ENCRYPTION_PACKET_FLAG = 0b_0000_1000;
        private const byte CHECKSUM_FLAG = 0b_0000_0100;
        private const byte TRAILING_LENGTH_FLAG = 0b_0000_0010;
        private const byte PADDING_FLAG = 0b_0000_0001;

        private static bool GetFlag(byte mask, byte flag)
        {
            return (mask & flag) > 0;
        }

        private static byte SetFlag(byte mask, byte flag, bool value)
        {
            if (value) { return Convert.ToByte(mask | flag); }
            return Convert.ToByte(mask & ~flag);
        }

        public uint LogicalRecordSegmentLength { get; set; }
        public byte LogicalRecordSegmentAttributes { get; set; }
        public ushort LogicalRecordType { get; set; }

        public bool LogicalRecordStructure
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, LOGICAL_RECORD_STRUCTURE_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, LOGICAL_RECORD_STRUCTURE_FLAG, value); }
        }

        public bool Predecessor
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, PREDECESSOR_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, PREDECESSOR_FLAG, value); }
        }

        public bool Successor
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, SUCCESSOR_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, SUCCESSOR_FLAG, value); }
        }

        public bool Encryption
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, ENCRYPTION_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, ENCRYPTION_FLAG, value); }
        }

        public bool EncryptionPacket
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, ENCRYPTION_PACKET_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, ENCRYPTION_PACKET_FLAG, value); }
        }

        public bool Checksum
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, CHECKSUM_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, CHECKSUM_FLAG, value); }
        }

        public bool TrailingLength
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, TRAILING_LENGTH_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, TRAILING_LENGTH_FLAG, value); }
        }

        public bool Padding
        {
            get { return GetFlag(LogicalRecordSegmentAttributes, PADDING_FLAG); }
            set { LogicalRecordSegmentAttributes = SetFlag(LogicalRecordSegmentAttributes, PADDING_FLAG, value); }
        }
    }
}
