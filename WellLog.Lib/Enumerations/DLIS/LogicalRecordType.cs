namespace WellLog.Lib.Enumerations.DLIS
{
    public enum ExplicitlyFormattedLogicalRecordType
    {
        FHLR = 0,
        OLR = 1,
        AXIS = 2,
        CHANNL = 3,
        FRAME = 4,
        STATIC = 5,
        SCRIPT = 6,
        UPDATE = 7,
        UDI = 8,
        LNAME = 9,
        SPEC = 10,
        DICT = 11
    }

    public enum IndirectlyFormattedLogicalRecordType
    {
        FDATA = 0,
        NOFORMAT = 1
    }
}
