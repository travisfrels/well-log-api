using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WellLog.Lib.Exceptions
{
    [Serializable]
    public class LasLogFormatException : Exception
    {
        public LasLogFormatException(string message)
            : base(message)
        {
        }

        protected LasLogFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
