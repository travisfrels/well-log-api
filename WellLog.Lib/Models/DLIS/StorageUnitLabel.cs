namespace WellLog.Lib.Models.DLIS
{
    public class StorageUnitLabel
    {
        private readonly char[] _sul;

        public string StorageUnitSequenceNumber => new string(_sul[0..4]);
        public string DlisVersion => new string(_sul[4..9]);
        public string StorageUnitStructure => new string(_sul[9..15]);
        public string MaximumRecordLength => new string(_sul[15..20]);
        public string StorageSetIdentifier => new string(_sul[20..80]);

        public StorageUnitLabel(string storageUnitLabel)
        {
            if (string.IsNullOrEmpty(storageUnitLabel)) { _sul = new char[0]; }
            else { _sul = storageUnitLabel.ToCharArray(); }
        }
    }
}
