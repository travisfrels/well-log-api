using WellLog.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WellLog.Lib.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        private readonly ValidationError[] _errors;

        public ValidationError[] Errors { get { return _errors; } }

        public ValidationException(IEnumerable<ValidationError> validationErrors)
            : base("One or more validation errors were found.")
        {
            if (validationErrors != null) { _errors = validationErrors.ToArray(); }
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _errors = (ValidationError[])info.GetValue("Errors", typeof(ValidationError[]));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Errors", _errors);
        }
    }
}
