using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IChannelBusiness
    {
        bool IsChannel(ExplicitlyFormattedLogicalRecord eflr);
        IEnumerable<ExplicitlyFormattedLogicalRecord> GetParameterEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs);
        IEnumerable<Channel> ConvertEFLRtoChannels(ExplicitlyFormattedLogicalRecord eflr);
    }
}