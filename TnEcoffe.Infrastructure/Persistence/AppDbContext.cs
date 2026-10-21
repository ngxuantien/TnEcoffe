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

        // ToList(): chụp danh sách trước, vì modelBuilder.Entity(...) bên dưới có thể thay đổi model đang duyệt
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().ToList())
        {
            var clrType = entityType.ClrType;

            // Chỉ cấu hình Id ở entity gốc, entity con kế thừa (TPH/TPT) dùng chung khoá của entity gốc
            if (typeof(BaseEntity).IsAssignableFrom(clrType) && entityType.BaseType is null)
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
