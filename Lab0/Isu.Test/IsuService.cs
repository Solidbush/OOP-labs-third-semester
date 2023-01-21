using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;
namespace Isu.Test;

public class TestIsuService
{
    private readonly IsuService isu;
    public TestIsuService()
    {
        isu = new IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var group = isu.AddGroup(new GroupName("M32111"), 3);

        var student = isu.AddStudent(group, "Alex", "Gavrilov", 324469);

        Assert.Contains(student, isu.FindStudents(group.NameOfGroup));
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var group = isu.AddGroup(new GroupName("M32111"), 2);
        var studentFirst = isu.AddStudent(group, "Alex", "Gavrilov", 324469);
        var stuudentSecond = isu.AddStudent(group, "Danil", "Muzikus", 324568);

        Assert.Throws<MaxStudentPerGroupException>(() => isu.AddStudent(group, "Ivan", "Ivanov", 696969));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameValidationException>(() => isu.AddGroup(new GroupName("32111"), 20));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var oldGroup = isu.AddGroup(new GroupName("M32111"), 2);
        var newGroup = isu.AddGroup(new GroupName("A33112"), 1);
        var student = isu.AddStudent(oldGroup, "Alex", "Gavrilov", 324469);

        isu.ChangeStudentGroup(student, newGroup);

        Assert.Contains(student, isu.FindStudents(newGroup.NameOfGroup));
        Assert.DoesNotContain(student, isu.FindStudents(oldGroup.NameOfGroup));
    }

    [Fact]
    public void AddGroupsToIsu_NullReference()
    {
        isu.AddGroup(new GroupName("M32112"), 3);
        isu.AddGroup(new GroupName("M32113"), 3);
        isu.AddGroup(new GroupName("M32114"), 3);

        var group = new Group(3, new GroupName("M32110"));

        Assert.Null(isu.FindGroup(group.NameOfGroup));
    }

    [Fact]
    public void AddGroupsToIsu_ComparedFoundGroupWithBaseGroup()
    {
        isu.AddGroup(new GroupName("M32112"), 3);
        isu.AddGroup(new GroupName("M32113"), 3);
        isu.AddGroup(new GroupName("M32114"), 3);

        var group = isu.AddGroup(new GroupName("M32111"), 3);

        Assert.Equal(group, isu.FindGroup(group.NameOfGroup));
    }
}