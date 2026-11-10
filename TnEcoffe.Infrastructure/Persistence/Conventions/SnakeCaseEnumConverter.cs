using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TnEcoffe.Infrastructure.Persistence.Conventions;

internal sealed class SnakeCaseEnumConverter<TEnum>() : ValueConverter<TEnum, string>(
    v => JsonNamingPolicy.SnakeCaseLower.ConvertName(v.ToString()),
    s => Enum.Parse<TEnum>(s.Replace("_", ""), true))
    where TEnum : struct, Enum;
