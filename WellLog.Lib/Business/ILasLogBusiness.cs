using System.IO;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public interface ILasLogBusiness
    {
        LasLog ReadStream(Stream lasStream);
        void WriteStream(Stream lasStream, LasLog lasLog);
    }
}