using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class TestIsuExtraService
{
    private readonly IsuExtraService _isuExtraService;

    public TestIsuExtraService()
    {
        _isuExtraService = new IsuExtraService();
    }

    [Fact]
    public void AddOgnpCourseFlowGroup_IsuExtraContainsCourseGroupFlow()
    {
        var course = _isuExtraService.AddOgnpCourse('A', 3);
        var coupleTimeOne = new CoupleTime(2);
        var coupleDayOne = new CoupleDay("Monday");
        var coupleTimeTwo = new CoupleTime(3);
        var coupleDayTwo = new CoupleDay("Friday");
        var teacher = new TeacherFullName("Alex", "Alex", "Alex");

        var coupleOne = new Couple(coupleTimeOne, teacher, coupleDayOne, 313);
        var coupleTwo = new Couple(coupleTimeTwo, teacher, coupleDayTwo, 313);
        TableOfCouple schedule = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        var flow = _isuExtraService.AddOgnpFlow(course, 2, 3, schedule);
        var group = _isuExtraService.AddOgnpGroup(course, flow, 5, 10);

        Assert.Contains(course, _isuExtraService.GetOgnpCourses());
        Assert.Contains(flow, _isuExtraService.GetOgnpFlowsInOgnpCourse(course));
        Assert.Contains(group, _isuExtraService.GetOgnpGroupsInOgnpFlow(course, flow));
    }

    [Fact]
    public void AddStudentInOgnpGroup_OgnpGroupContainsStudentStudentHasOneCourse()
    {
        var course = _isuExtraService.AddOgnpCourse('A', 3);
        var coupleTimeOne = new CoupleTime(2);
        var coupleDayOne = new CoupleDay("Monday");
        var coupleTimeTwo = new CoupleTime(3);
        var coupleDayTwo = new CoupleDay("Friday");
        var teacher = new TeacherFullName("Alex", "Alex", "Alex");
        var coupleTimeTree = new CoupleTime(4);
        var coupleDayTree = new CoupleDay("Monday");

        var coupleOne = new Couple(coupleTimeOne, teacher, coupleDayOne, 313);
        var coupleTwo = new Couple(coupleTimeTwo, teacher, coupleDayTwo, 313);
        var coupleTree = new Couple(coupleTimeTree, teacher, coupleDayTree, 314);
        TableOfCouple schedule = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        TableOfCouple scheduleForCommonGroup = new TableOfCouple(new List<Couple>() { coupleTree });
        var flow = _isuExtraService.AddOgnpFlow(course, 2, 3, schedule);
        var group = _isuExtraService.AddOgnpGroup(course, flow, 5, 10);
        var comonGroup = _isuExtraService.AddGroup(new GroupName("M32111"), 10, scheduleForCommonGroup);
        var student = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324469);
        student = _isuExtraService.EnrollStudentOnOgnpCourse(student, comonGroup, course, flow, group);

        Assert.Contains(student, _isuExtraService.FindOgnpGroup(course, flow, group).GetStudents());
        Assert.Equal(1, _isuExtraService.HuwMuchOgnpHasStudent(student.Id));
    }

    [Fact]
    public void DeleteStudentFromOgnpGrou_OgnpGroupNotContainsStudent()
    {
        var course = _isuExtraService.AddOgnpCourse('A', 3);
        var coupleTimeOne = new CoupleTime(2);
        var coupleDayOne = new CoupleDay("Monday");
        var coupleTimeTwo = new CoupleTime(3);
        var coupleDayTwo = new CoupleDay("Friday");
        var teacher = new TeacherFullName("Alex", "Alex", "Alex");
        var coupleTimeTree = new CoupleTime(4);
        var coupleDayTree = new CoupleDay("Monday");

        var coupleOne = new Couple(coupleTimeOne, teacher, coupleDayOne, 313);
        var coupleTwo = new Couple(coupleTimeTwo, teacher, coupleDayTwo, 313);
        var coupleTree = new Couple(coupleTimeTree, teacher, coupleDayTree, 314);
        TableOfCouple schedule = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        TableOfCouple scheduleForCommonGroup = new TableOfCouple(new List<Couple>() { coupleTree });
        var flow = _isuExtraService.AddOgnpFlow(course, 2, 3, schedule);
        var group = _isuExtraService.AddOgnpGroup(course, flow, 5, 10);
        var comonGroup = _isuExtraService.AddGroup(new GroupName("M32111"), 10, scheduleForCommonGroup);
        var student = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324469);
        _isuExtraService.UnsubscribeStudentFromOgnpCourse(student, comonGroup, course, flow, group);

        Assert.DoesNotContain(student, _isuExtraService.FindOgnpGroup(course, flow, group).GetStudents());
        Assert.Equal(0, _isuExtraService.HuwMuchOgnpHasStudent(student.Id));
    }

    [Fact]
    public void CreateOgnpCourseWithFlows_CourseShouldContainsFlows()
    {
        var course = _isuExtraService.AddOgnpCourse('A', 3);
        var coupleTimeOne = new CoupleTime(2);
        var coupleDayOne = new CoupleDay("Monday");
        var coupleTimeTwo = new CoupleTime(3);
        var coupleDayTwo = new CoupleDay("Friday");
        var teacher = new TeacherFullName("Alex", "Alex", "Alex");
        var coupleTimeTree = new CoupleTime(4);
        var coupleDayTree = new CoupleDay("Monday");

        var coupleOne = new Couple(coupleTimeOne, teacher, coupleDayOne, 313);
        var coupleTwo = new Couple(coupleTimeTwo, teacher, coupleDayTwo, 313);
        var coupleTree = new Couple(coupleTimeTree, teacher, coupleDayTree, 314);
        TableOfCouple scheduleOne = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        TableOfCouple scheduleTwo = new TableOfCouple(new List<Couple>() { coupleTree });
        var flowOne = _isuExtraService.AddOgnpFlow(course, 2, 3, scheduleOne);
        var flowTwo = _isuExtraService.AddOgnpFlow(course, 3, 3, scheduleTwo);
        IReadOnlyCollection<OgnpFlow> flows = _isuExtraService.GetOgnpFlowsInOgnpCourse(course);

        Assert.Contains(flowOne, flows);
        Assert.Contains(flowTwo, flows);
    }

    [Fact]
    public void MakeGroupOgnpWithStudents_OgnpGroupshouldContainStudents()
    {
        var course = _isuExtraService.AddOgnpCourse('A', 3);
        var coupleTimeOne = new CoupleTime(2);
        var coupleDayOne = new CoupleDay("Monday");
        var coupleTimeTwo = new CoupleTime(3);
        var coupleDayTwo = new CoupleDay("Friday");
        var teacher = new TeacherFullName("Alex", "Alex", "Alex");
        var coupleTimeTree = new CoupleTime(4);
        var coupleDayTree = new CoupleDay("Monday");

        var coupleOne = new Couple(coupleTimeOne, teacher, coupleDayOne, 313);
        var coupleTwo = new Couple(coupleTimeTwo, teacher, coupleDayTwo, 313);
        var coupleTree = new Couple(coupleTimeTree, teacher, coupleDayTree, 314);
        TableOfCouple scheduleOne = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        TableOfCouple scheduleTwo = new TableOfCouple(new List<Couple>() { coupleTree });
        var flowOne = _isuExtraService.AddOgnpFlow(course, 2, 3, scheduleOne);
        var group = _isuExtraService.AddOgnpGroup(course, flowOne, 5, 3);
        var comonGroup = _isuExtraService.AddGroup(new GroupName("M32111"), 10, scheduleTwo);
        var student1 = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324469);
        var student2 = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324468);
        var student3 = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324467);
        student1 = _isuExtraService.EnrollStudentOnOgnpCourse(student1, comonGroup, course, flowOne, group);
        student1 = _isuExtraService.EnrollStudentOnOgnpCourse(student2, comonGroup, course, flowOne, group);
        student1 = _isuExtraService.EnrollStudentOnOgnpCourse(student3, comonGroup, course, flowOne, group);

        Assert.Contains(student1, _isuExtraService.FindOgnpGroup(course, flowOne, group).GetStudents());
        Assert.Contains(student2, _isuExtraService.FindOgnpGroup(course, flowOne, group).GetStudents());
        Assert.Contains(student3, _isuExtraService.FindOgnpGroup(course, flowOne, group).GetStudents());
    }

    [Fact]
    public void FindStudentsWithOutOgnpInGroup_ListWithThisStudents()
    {
        var course1 = _isuExtraService.AddOgnpCourse('A', 3);
        var course2 = _isuExtraService.AddOgnpCourse('B', 3);
        var course3 = _isuExtraService.AddOgnpCourse('C', 3);
        var course4 = _isuExtraService.AddOgnpCourse('D', 3);
        var coupleTimeOne = new CoupleTime(2);
        var coupleDayOne = new CoupleDay("Monday");
        var coupleTimeTwo = new CoupleTime(3);
        var coupleDayTwo = new CoupleDay("Friday");
        var teacher = new TeacherFullName("Alex", "Alex", "Alex");
        var coupleTimeTree = new CoupleTime(4);
        var coupleDayTree = new CoupleDay("Monday");
        var coupleOne = new Couple(coupleTimeOne, teacher, coupleDayOne, 313);
        var coupleTwo = new Couple(coupleTimeTwo, teacher, coupleDayTwo, 313);
        var coupleTree = new Couple(coupleTimeTree, teacher, coupleDayTree, 314);
        TableOfCouple scheduleOne = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        TableOfCouple scheduleTwo = new TableOfCouple(new List<Couple>() { coupleOne, coupleTwo });
        TableOfCouple scheduleThree = new TableOfCouple(new List<Couple>() { coupleTree });
        var flowOne = _isuExtraService.AddOgnpFlow(course1, 2, 3, scheduleOne);
        var flowTwo = _isuExtraService.AddOgnpFlow(course2, 2, 3, scheduleTwo);
        var flowThree = _isuExtraService.AddOgnpFlow(course3, 2, 3, scheduleOne);
        var flowFour = _isuExtraService.AddOgnpFlow(course4, 2, 3, scheduleTwo);
        var group1 = _isuExtraService.AddOgnpGroup(course1, flowOne, 5, 3);
        var group2 = _isuExtraService.AddOgnpGroup(course2, flowTwo, 5, 3);
        var group3 = _isuExtraService.AddOgnpGroup(course3, flowThree, 5, 3);
        var group4 = _isuExtraService.AddOgnpGroup(course4, flowFour, 5, 3);
        var comonGroup = _isuExtraService.AddGroup(new GroupName("M32111"), 10, scheduleThree);
        var student1 = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324469);
        var student2 = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324468);
        var student3 = _isuExtraService.AddStudent(comonGroup, "Alex", "Gavrilov", 324467);
        student1 = _isuExtraService.EnrollStudentOnOgnpCourse(student1, comonGroup, course1, flowOne, group1);
        student2 = _isuExtraService.EnrollStudentOnOgnpCourse(student2, comonGroup, course2, flowTwo, group2);
        student1 = _isuExtraService.EnrollStudentOnOgnpCourse(student1, comonGroup, course3, flowThree, group3);
        student2 = _isuExtraService.EnrollStudentOnOgnpCourse(student2, comonGroup, course4, flowFour, group4);
        var studentsWithOutOgnp = _isuExtraService.StudentsWithOutOgnpInGroup(comonGroup);

        Assert.Contains(student3, studentsWithOutOgnp);
        Assert.Single(studentsWithOutOgnp);
    }
}