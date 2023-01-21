using System.Linq.Expressions;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class CoupleTime : IEquatable<CoupleTime>
{
    private const int MaxCoupleNumber = 8;
    private const int MinCoupleNumber = 1;

    public CoupleTime(int numberOfCouple)
    {
        if (numberOfCouple is > MaxCoupleNumber or < MinCoupleNumber)
            throw new CoupleTimeValidationException($"Correct time for couple from 1 to 8. Your time: {numberOfCouple}");

        NumberCouple = numberOfCouple;
    }

    public int NumberCouple { get; }

    public bool Equals(CoupleTime other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return NumberCouple == other.NumberCouple;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CoupleTime)obj);
    }

    public override int GetHashCode()
    {
        return NumberCouple;
    }
}