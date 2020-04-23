using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IChannelBusiness
    {
        bool IsChannel(ExplicitlyFormattedLogicalRecord eflr);
        IEnumerable<ExplicitlyFormattedLogicalRecord> GetChannelEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs);
        IEnumerable<Channel> ConvertEFLRtoChannels(ExplicitlyFormattedLogicalRecord eflr);
    }
}