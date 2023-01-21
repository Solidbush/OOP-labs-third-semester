using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class CoupleDay : IEquatable<CoupleDay>
{
    private List<string> _days = new List<string>()
        { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    public CoupleDay(string day)
    {
        EnsureCoupleDay(day);
        Day = day;
    }

    public string Day { get; }

    public void EnsureCoupleDay(string day)
    {
        if (string.IsNullOrWhiteSpace(day))
            throw new NullReferenceException();
        if (!_days.Contains(day))
            throw new CorrectDayException($"{day} is uncorrect name of the day");
    }

    public bool Equals(CoupleDay other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Day == other.Day;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((CoupleDay)obj);
    }

    public override int GetHashCode()
    {
        return Day.GetHashCode();
    }
}