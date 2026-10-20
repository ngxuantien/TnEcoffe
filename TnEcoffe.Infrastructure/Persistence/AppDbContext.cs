using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Interface;

namespace TnEcoffe.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            if (typeof(BaseEntity).IsAssignableFrom(clrType))
            {
                modelBuilder.Entity(clrType)
                    .Property(nameof(BaseEntity.Id))
                    .ValueGeneratedNever();
            }

            if (typeof(ISoftDeletable).IsAssignableFrom(clrType) && entityType.BaseType is null)
            {
                modelBuilder.Entity(clrType).HasQueryFilter(BuildSoftDeleteFilter(clrType));
            }
        }
    }

    private static LambdaExpression BuildSoftDeleteFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var deletedAt = Expression.Property(parameter, nameof(ISoftDeletable.DeletedAt));
        var isNull = Expression.Equal(deletedAt, Expression.Constant(null, typeof(DateTime?)));

        return Expression.Lambda(isNull, parameter);
    }
}
