using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IComponentReaderFactory
    {
        IComponentReader GetReader(ComponentDescriptor descriptor);
    }
}