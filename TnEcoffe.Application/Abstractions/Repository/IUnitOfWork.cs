using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Application.Abstractions.Repository;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);

    Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default);

    Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<TResult>> operation,
        CancellationToken ct = default);
}
