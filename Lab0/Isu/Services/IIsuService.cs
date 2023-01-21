using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    Group AddGroup(GroupName name, int capacity);
    Student AddStudent(Group group, string firstName, string secondName, int id);

    Student GetStudent(int id);
    Student FindStudent(int id);
    IEnumerable<Student> FindStudents(GroupName groupName);
    IEnumerable<Student> FindStudents(CourseNumber courseNumber);

    Group FindGroup(GroupName groupName);
    IEnumerable<Group> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}