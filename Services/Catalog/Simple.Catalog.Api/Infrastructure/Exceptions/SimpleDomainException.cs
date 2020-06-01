namespace Simple.Catalog.Api.Infrastructure.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class SimpleDomainException : Exception
    {
        public SimpleDomainException()
        {
        }

        public SimpleDomainException(string message) : base(message)
        {
        }

        public SimpleDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
