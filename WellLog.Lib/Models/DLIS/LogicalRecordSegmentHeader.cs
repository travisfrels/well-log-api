using WellLog.Lib.Helpers;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentHeader
    {
        public const byte LOGICAL_RECORD_STRUCTURE_MASK = 0b_1000_0000;
        public const byte PREDECESSOR_MASK = 0b_0100_0000;
        public const byte SUCCESSOR_MASK = 0b_0010_0000;
        public const byte ENCRYPTION_MASK = 0b_0001_0000;
        public const byte ENCRYPTION_PACKET_MASK = 0b_0000_1000;
        public const byte CHECKSUM_MASK = 0b_0000_0100;
        public const byte TRAILING_LENGTH_MASK = 0b_0000_0010;
        public const byte PADDING_MASK = 0b_0000_0001;

        public uint LogicalRecordSegmentLength { get; set; }
        public byte LogicalRecordSegmentAttributes { get; set; }
        public ushort LogicalRecordType { get; set; }

        public bool LogicalRecordStructure
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(LOGICAL_RECORD_STRUCTURE_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(LOGICAL_RECORD_STRUCTURE_MASK, value); }
        }

        public bool Predecessor
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(PREDECESSOR_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(PREDECESSOR_MASK, value); }
        }

        public bool Successor
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(SUCCESSOR_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(SUCCESSOR_MASK, value); }
        }

        public bool Encryption
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(ENCRYPTION_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(ENCRYPTION_MASK, value); }
        }

        public bool EncryptionPacket
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(ENCRYPTION_PACKET_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(ENCRYPTION_PACKET_MASK, value); }
        }

        public bool Checksum
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(CHECKSUM_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(CHECKSUM_MASK, value); }
        }

        public bool TrailingLength
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(TRAILING_LENGTH_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(TRAILING_LENGTH_MASK, value); }
        }

        public bool Padding
        {
            get { return LogicalRecordSegmentAttributes.GetBitUsingMask(PADDING_MASK); }
            set { LogicalRecordSegmentAttributes = LogicalRecordSegmentAttributes.AssignBitUsingMask(PADDING_MASK, value); }
        }
    }
}
