using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WellLog.Lib.Exceptions
{
    [Serializable]
    public class LasLineException : Exception
    {
        public string LasLine { get; private set; }

        public LasLineException(string message, string lasLine)
            : base(message)
        {
            if (lasLine != null) { LasLine = lasLine; }
        }

        protected LasLineException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            LasLine = (string)info.GetValue("LasLine", typeof(string));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("LasLine", LasLine);
        }
    }
}
