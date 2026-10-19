using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;

namespace TnEcoffe.Application.Abstractions.Repository;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Query();

    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}
