using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class Couple : IEquatable<Couple>
{
    private const int MinClassroomNumber = 0;
    public Couple(CoupleTime coupleTime, TeacherFullName teacher, CoupleDay day, int classroomNumber)
    {
        EnsureClassroomNumber(classroomNumber);
        CoupleTime = coupleTime;
        TeacherName = teacher;
        Day = day;
        ClassroomNumber = classroomNumber;
    }

    public CoupleTime CoupleTime { get; }
    public TeacherFullName TeacherName { get; }
    public CoupleDay Day { get; }
    public int ClassroomNumber { get; }

    public void EnsureClassroomNumber(int classroomNumber)
    {
        if (classroomNumber < MinClassroomNumber)
            throw new ClassroomNumberValidationException($"Number of classroom should be more {MinClassroomNumber}");
    }

    public bool Equals(Couple other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CoupleTime.Equals(other.CoupleTime) && Day.Equals(other.Day);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Couple)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CoupleTime, Day);
    }
}