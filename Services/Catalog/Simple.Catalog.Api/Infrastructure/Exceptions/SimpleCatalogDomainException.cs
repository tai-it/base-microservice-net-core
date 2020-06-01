namespace Simple.Catalog.Api.Infrastructure.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class SimpleCatalogDomainException : Exception
    {
        public SimpleCatalogDomainException()
        {
        }

        public SimpleCatalogDomainException(string message) : base(message)
        {
        }

        public SimpleCatalogDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleCatalogDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
