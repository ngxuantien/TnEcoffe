using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entites.Settings;

public sealed class SpecialDay : BaseEntity
{
    public DateOnly Date { get; private set; }

    public bool IsClosed { get; private set; }

    public TimeOnly? OpenTime { get; private set; }

    public TimeOnly? CloseTime { get; private set; }

    public string? Note { get; private set; }

    private SpecialDay() { }

    public SpecialDay(DateOnly date, bool isClosed, TimeOnly? openTime, TimeOnly? closeTime, string? note)
    {
        Date = date;
        Update(isClosed, openTime, closeTime, note);
    }

    public void Update(bool isClosed, TimeOnly? openTime, TimeOnly? closeTime, string? note)
    {
        if (!isClosed && (openTime is null || closeTime is null || closeTime <= openTime))
            throw new DomainException(ErrorCodes.Settings.SpecialDayInvalid, "Giờ của ngày đặc biệt không hợp lệ.");

        IsClosed = isClosed;
        OpenTime = isClosed ? null : openTime;
        CloseTime = isClosed ? null : closeTime;
        Note = note;
    }
}
