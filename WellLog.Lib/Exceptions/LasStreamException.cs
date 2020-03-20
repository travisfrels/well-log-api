using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WellLog.Lib.Exceptions
{
    [Serializable]
    public class LasStreamException : Exception
    {
        public LasStreamException(string message) : base(message) { }

        protected LasStreamException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
