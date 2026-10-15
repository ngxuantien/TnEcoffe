using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common
{
    public sealed class DomainException(string code, string message) : Exception(message)
    {
        public string Code { get; } = code;
    }
}
