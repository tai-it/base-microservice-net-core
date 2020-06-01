namespace Simple.Identity.Api.Infrastructure.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class SimpleIdentityDomainException : Exception
    {
        public SimpleIdentityDomainException()
        {
        }

        public SimpleIdentityDomainException(string message) : base(message)
        {
        }

        public SimpleIdentityDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleIdentityDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
