namespace WellLog.Lib.Models.DLIS
{
    public abstract class ComponentBase
    {
        public ComponentDescriptor Descriptor { get; set; }
        public long StartPosition { get; set; }
        public ComponentBase Template { get; set; }

    }
}
