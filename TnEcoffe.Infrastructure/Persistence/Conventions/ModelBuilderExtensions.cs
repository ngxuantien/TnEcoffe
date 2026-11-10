using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Interface;

namespace TnEcoffe.Infrastructure.Persistence.Conventions;

internal static class ModelBuilderExtensions
{
    public static void ApplyBaseEntityConventions(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().ToList())
        {
            var clrType = entityType.ClrType;

            if (entityType.BaseType is not null)
                continue;

            var builder = modelBuilder.Entity(clrType);

            if (typeof(BaseEntity).IsAssignableFrom(clrType))
                builder.Property(nameof(BaseEntity.Id)).ValueGeneratedNever();

            if (typeof(AuditableEntity).IsAssignableFrom(clrType))
                builder.Property(nameof(AuditableEntity.Version)).IsRowVersion();

            if (typeof(ISoftDeletable).IsAssignableFrom(clrType))
                builder.HasQueryFilter(BuildSoftDeleteFilter(clrType));
        }
    }

    public static void ApplyEnumConventions(this ModelBuilder modelBuilder)
    {
        static string Snake(string s) => JsonNamingPolicy.SnakeCaseLower.ConvertName(s);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetDeclaredProperties())
            {
                var enumType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;

                if (!enumType.IsEnum
                    || property.GetValueConverter() is not null
                    || property.GetProviderClrType() is not null)
                    continue;

                var converter = (ValueConverter)Activator.CreateInstance(
                    typeof(SnakeCaseEnumConverter<>).MakeGenericType(enumType))!;

                property.SetValueConverter(converter);
                property.SetMaxLength(30);

                var table = entityType.GetTableName();
                var column = property.GetColumnName();
                var values = string.Join(", ", Enum.GetNames(enumType).Select(n => $"'{Snake(n)}'"));

                entityType.AddCheckConstraint($"ck_{table}_{column}", $"{column} IN ({values})");
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
