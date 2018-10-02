using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicViewModelConverter
{
    [Serializable]
    public class UnhandledConversion : Exception
    {
        public UnhandledConversion()
        {
        }
        public UnhandledConversion(string sourceType, string targetType)
        {
            string ErrorMessage = "Unhandled conversion From Type: " + sourceType + " to: " + targetType;
            throw new UnhandledConversion(ErrorMessage);
        }
        public UnhandledConversion(string message) : base(message)
        {
        }

        public UnhandledConversion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnhandledConversion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}