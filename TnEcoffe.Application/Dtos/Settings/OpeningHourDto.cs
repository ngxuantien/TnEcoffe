using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Application.Dtos.Settings;

public sealed record OpeningHourDto(int DayOfWeek, bool IsOpen, string? OpenTime, string? CloseTime);
