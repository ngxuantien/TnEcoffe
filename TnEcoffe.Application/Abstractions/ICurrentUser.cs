using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Application.Abstractions
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }

        bool IsAuthenticated { get; }

        bool IsInRole(string role);
    }
}
