using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TnEcoffe.Domain.Common;

namespace TnEcoffe.Application.Abstractions.Repository;

public interface IReadRepository<T> where T : BaseEntity
{
    IQueryable<T> Query();

    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
}
