using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common.Interface
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }

        DateTime? DeletedAt { get; }

        Guid? DeletedBy { get; }
    }
}
