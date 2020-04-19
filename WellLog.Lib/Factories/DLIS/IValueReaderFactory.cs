using WellLog.Lib.Enumerations.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IValueReaderFactory
    {
        IValueReader GetReader(RepresentationCode r);
    }
}