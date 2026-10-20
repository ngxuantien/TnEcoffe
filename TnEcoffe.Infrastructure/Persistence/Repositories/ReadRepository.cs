using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TnEcoffe.Application.Abstractions.Repositories;
using TnEcoffe.Domain.Common;

namespace TnEcoffe.Infrastructure.Persistence.Repositories;

internal sealed class ReadRepository<T>(AppDbContext db) : IReadRepository<T>
    where T : BaseEntity
{
    private readonly DbSet<T> _set = db.Set<T>();

    public IQueryable<T> Query() => _set.AsNoTracking();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => _set.AnyAsync(predicate, ct);
}
