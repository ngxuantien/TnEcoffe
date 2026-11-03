using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entites.Settings;

public sealed class OpeningHour : BaseEntity
{
    public DayOfWeek DayOfWeek { get; private set; }

    public bool IsOpen { get; private set; }

    public TimeOnly? OpenTime { get; private set; }

    public TimeOnly? CloseTime { get; private set; }

    private OpeningHour() { }

    public void Update(bool isOpen, TimeOnly? openTime, TimeOnly? closeTime)
    {
        if (isOpen && (openTime is null || closeTime is null || closeTime <= openTime))
            throw new DomainException(ErrorCodes.Settings.OpeningHourInvalid, "Giờ mở cửa không hợp lệ.");

        IsOpen = isOpen;
        OpenTime = isOpen ? openTime : null;
        CloseTime = isOpen ? closeTime : null;
    }

    public bool IsOpenAt(TimeOnly time) => IsOpen && time >= OpenTime && time < CloseTime;
}
