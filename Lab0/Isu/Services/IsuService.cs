using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private static Dictionary<Group, List<Student>> studentsByGroup;

    public IsuService()
    {
        studentsByGroup = new Dictionary<Group, List<Student>>();
    }

    public Group AddGroup(GroupName name, int capacity)
    {
        var tempGroup = new Group(capacity, name);
        if (studentsByGroup.Keys.Any(group => group.NameOfGroup.NameOfGroup == name.NameOfGroup))
        {
            throw new AddGroupException($"Group with name: {name} already exist");
        }

        studentsByGroup.Add(tempGroup, new List<Student>(tempGroup.GroupCapacity));
        return tempGroup;
    }

    public Student AddStudent(Group group, string firstName, string secondName, int id)
    {
        if (FindStudent(id) != null)
        {
            throw new AddStudentException($"Student with id: {id} already exists");
        }

        if (studentsByGroup[group].Count + 1 > group.GroupCapacity)
        {
            throw new MaxStudentPerGroupException($"Group {group.NameOfGroup} crowded");
        }

        var student = new Student(id, firstName, secondName);
        studentsByGroup[group].Add(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        return studentsByGroup
                   .Values
                   .SelectMany(x => x)
                   .SingleOrDefault(student => student.Id == id)
               ?? throw new GetStudentException($"Student with {id} not found");
    }

    public Student FindStudent(int id)
    {
        try
        {
            return GetStudent(id: id);
        }
        catch
        {
            return null;
        }
    }

    public IEnumerable<Student> FindStudents(GroupName groupName)
    {
        return studentsByGroup
            .Where(pair => pair.Key.NameOfGroup == groupName)
            .SelectMany(pair => pair.Value);
    }

    public IEnumerable<Student> FindStudents(CourseNumber courseNumber)
    {
        return studentsByGroup
            .Where(pair => pair.Key.NameOfGroup.Course == courseNumber)
            .SelectMany(x => x.Value);
    }

    public Group FindGroup(GroupName groupName)
    {
        return studentsByGroup
            .Keys
            .FirstOrDefault(x => x.NameOfGroup == groupName);
    }

    public IEnumerable<Group> FindGroups(CourseNumber courseNumber)
    {
        return studentsByGroup
            .Where(pair => pair.Key.NameOfGroup.Course == courseNumber)
            .Select(pair => pair.Key);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (FindStudent(student.Id).Id != student.Id)
        {
            throw new StudentExistingException($"Student with Id: {student.Id} doesn't exists");
        }

        if (FindGroup(newGroup.NameOfGroup).NameOfGroup.NameOfGroup != newGroup.NameOfGroup.NameOfGroup)
        {
            throw new GroupExistingExeption($"Group with name: {newGroup.NameOfGroup.NameOfGroup} doesn't exists");
        }

        if (studentsByGroup[newGroup].Count + 1 > newGroup.GroupCapacity)
        {
            throw new MaxStudentPerGroupException($"The group {newGroup.NameOfGroup.NameOfGroup} is already full");
        }

        Group oldGroup = studentsByGroup
            .FirstOrDefault(pair => pair.Value.Contains(student)).Key;
        studentsByGroup[oldGroup].Remove(student);
        studentsByGroup[newGroup].Add(student);
    }
}