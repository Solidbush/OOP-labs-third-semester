using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class OgnpGroup : IEquatable<OgnpGroup>
{
    private const int MinNumberOfGroup = 1;
    private const int MinNumberOfStudentsInOgnpGroup = 1;
    private readonly List<Student> _listStudent;

    public OgnpGroup(int groupNumber, int maxCountOfStudentsInOgnpGroup)
    {
        EnsureMaxCountStudentsInOgnpGroup(maxCountOfStudentsInOgnpGroup);
        EnsureOgnpGroupNumber(groupNumber);
        GroupNumber = groupNumber;
        _listStudent = new List<Student>();
        Capacity = maxCountOfStudentsInOgnpGroup;
    }

    public int GroupNumber { get; }
    public int Capacity { get; }

    public void EnsureMaxCountStudentsInOgnpGroup(int maxCountOfStudents)
    {
        if (maxCountOfStudents < MinNumberOfStudentsInOgnpGroup)
            throw new MaxCountOfStudentsInGroupException($"Max count of students in group should be more then {MinNumberOfStudentsInOgnpGroup}");
    }

    public void EnsureOgnpGroupNumber(int groupNumber)
    {
        if (groupNumber < MinNumberOfGroup)
            throw new NumberOfGroupValidationException($"Number of group should be more then {MinNumberOfGroup}");
    }

    public IReadOnlyCollection<Student> GetStudents()
    {
        return _listStudent;
    }

    public bool Equals(OgnpGroup other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GroupNumber == other.GroupNumber;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OgnpGroup)obj);
    }

    public override int GetHashCode()
    {
        return GroupNumber;
    }

    internal void AddStudent(Student newStudent)
    {
        if (newStudent == null)
            throw new ArgumentNullException(nameof(newStudent));
        _listStudent.Add(newStudent);
    }

    internal void DeleteStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));
        _listStudent.Remove(student);
    }
}