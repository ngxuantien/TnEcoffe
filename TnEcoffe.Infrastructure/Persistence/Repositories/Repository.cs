using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Application.Abstractions.Repositories;
using TnEcoffe.Domain.Common;

namespace TnEcoffe.Infrastructure.Persistence.Repositories;

internal sealed class Repository<T>(AppDbContext db) : IRepository<T>
    where T : BaseEntity
{
    private readonly DbSet<T> _set = db.Set<T>();
    public IQueryable<T> Query() => _set;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _set.FindAsync([id], ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
        => await _set.AddAsync(entity, ct);

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        => _set.AddRangeAsync(entities, ct);

    public void Remove(T entity) => _set.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities) => _set.RemoveRange(entities);
}
