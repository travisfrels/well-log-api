using WellLog.Lib.Models;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WellLog.Lib.Exceptions
{
    [Serializable]
    public class LasLogFormatException : Exception
    {
        public LasLog Log { get; private set; }

        public LasLogFormatException(string message, LasLog log)
            : base(message)
        {
            if (log != null) { Log = log; }
        }

        protected LasLogFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Log = (LasLog)info.GetValue("Log", typeof(LasLog));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Log", Log);
        }
    }
}
