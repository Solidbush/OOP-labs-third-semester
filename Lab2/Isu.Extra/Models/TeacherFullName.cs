namespace Isu.Extra.Models;

public class TeacherFullName : IEquatable<TeacherFullName>
{
    public TeacherFullName(string firstName, string lastName, string middleName)
    {
        EnsureFullName(firstName, lastName, middleName);
        FirstName = firstName;
        MidleName = middleName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string MidleName { get; }
    public string LastName { get; }

    public void EnsureFullName(string firstName, string lastName, string middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName));
        if (string.IsNullOrWhiteSpace(middleName))
            throw new ArgumentNullException(nameof(middleName));
    }

    public bool Equals(TeacherFullName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FirstName == other.FirstName && MidleName == other.MidleName && LastName == other.LastName;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((TeacherFullName)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, MidleName, LastName);
    }
}