using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; private set; }

    public Guid? CreatedBy { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public Guid? UpdatedBy { get; private set; }
}
