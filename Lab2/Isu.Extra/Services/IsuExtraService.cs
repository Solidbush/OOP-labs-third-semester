using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private const int MaxCountOfOgnpCoursesForOneStudent = 2;
    private readonly IsuService _isuService;
    private readonly List<OgnpCourse> _ognpCourses;
    private readonly Dictionary<Group, TableOfCouple> _schedule;

    public IsuExtraService()
    {
        _isuService = new IsuService();
        _ognpCourses = new List<OgnpCourse>();
        _schedule = new Dictionary<Group, TableOfCouple>();
    }

    public Group AddGroup(GroupName groupName, int capacity, TableOfCouple schedule)
    {
        Group tempGroup = _isuService.AddGroup(groupName, capacity);
        _schedule.Add(tempGroup, schedule);
        return tempGroup;
    }

    public Student AddStudent(Group group, string firstName, string secondName, int id)
    {
        return _isuService.AddStudent(group, firstName, secondName, id);
    }

    public OgnpCourse FindOgnpCourse(OgnpCourse ognpCourse)
    {
        return _ognpCourses.SingleOrDefault(course => course.Specialization == ognpCourse.Specialization);
    }

    public OgnpFlow FindOgnpFlow(OgnpCourse ognpCourse, OgnpFlow ognpFlow)
    {
        try
        {
            FindOgnpCourse(ognpCourse);
            int ognpCourseIndex = _ognpCourses.IndexOf(ognpCourse);
            if (_ognpCourses[ognpCourseIndex].GetOgnpFlows().Contains(ognpFlow) != true)
                throw new FindOgnpFlowException($"Ognp flow with number {ognpFlow.FlowNumber} doesn't exists in course {ognpCourse.Specialization}");
            return ognpFlow;
        }
        catch
        {
            return null;
        }
    }

    public OgnpGroup FindOgnpGroup(OgnpCourse ognpCourse, OgnpFlow ognpFlow, OgnpGroup ognpGroup)
    {
        try
        {
            FindOgnpCourse(ognpCourse);
            var tempOgnpFlow = FindOgnpFlow(ognpCourse, ognpFlow);
            if (tempOgnpFlow.GetGroupsInOgnpFlow().Contains(ognpGroup) != true)
                throw new FindOgnpGroupException($"Ognp group with number {ognpGroup.GroupNumber} doesn't exists in flow {ognpFlow.FlowNumber} ");
            return ognpGroup;
        }
        catch
        {
            return null;
        }
    }

    public OgnpCourse AddOgnpCourse(char specialization, int countOfFlows)
    {
        OgnpCourse tempOgnpCourse = new OgnpCourse(specialization, countOfFlows);
        if (FindOgnpCourse(tempOgnpCourse) != null)
            throw new AddOgnpCourseException($"Course with specialization {specialization} already exists");
        _ognpCourses.Add(tempOgnpCourse);
        return tempOgnpCourse;
    }

    public OgnpFlow AddOgnpFlow(OgnpCourse ognpCourse, int flowNumber, int maxCountOfGroups, TableOfCouple schedule)
    {
        OgnpFlow tempOgnpFlow = new OgnpFlow(flowNumber, maxCountOfGroups, schedule);
        if (FindOgnpCourse(ognpCourse) == null)
            throw new AddOgnpFlowException($"OGNP course with specialization {ognpCourse.Specialization} doesn't exists");
        if (FindOgnpFlow(ognpCourse, tempOgnpFlow) != null)
            throw new AddOgnpCourseException($"Flow with {flowNumber} in course with specialization {ognpCourse.Specialization} already exists");
        int ognpCourseIndex = _ognpCourses.IndexOf(ognpCourse);
        if (_ognpCourses[ognpCourseIndex].GetOgnpFlows().Count + 1 > _ognpCourses[ognpCourseIndex].Capacity)
            throw new AddOgnpFlowException($"In course with specialization {ognpCourse.Specialization} already contains max count of flows");
        _ognpCourses[ognpCourseIndex].AddFlow(tempOgnpFlow);
        return tempOgnpFlow;
    }

    public OgnpGroup AddOgnpGroup(
        OgnpCourse ognpCourse,
        OgnpFlow ognpFlow,
        int groupNumber,
        int maxCountOfStudentsInOgnpGroup)
    {
        var tempOgnpGroup = new OgnpGroup(groupNumber, maxCountOfStudentsInOgnpGroup);
        if (FindOgnpCourse(ognpCourse) == null)
            throw new AddOgnpGroupException($"OGNP course with specialization {ognpCourse.Specialization} doesn't exists");
        if (FindOgnpFlow(ognpCourse, ognpFlow) == null)
            throw new AddOgnpGroupException($"Flow with {ognpFlow.FlowNumber} in course with specialization {ognpCourse.Specialization} doesn't exists");
        if (FindOgnpGroup(ognpCourse, ognpFlow, tempOgnpGroup) != null)
            throw new AddOgnpGroupException($"Ognp group with number {tempOgnpGroup.GroupNumber} already exists");
        if (FindOgnpFlow(ognpCourse, ognpFlow).GetGroupsInOgnpFlow().Count + 1 <= FindOgnpFlow(ognpCourse, ognpFlow).Capacity)
            FindOgnpFlow(ognpCourse, ognpFlow).AddGroup(tempOgnpGroup);
        else
            throw new AddOgnpGroupException($"Flow {ognpFlow.FlowNumber} already has max count of groups");
        return tempOgnpGroup;
    }

    public IReadOnlyCollection<OgnpCourse> GetOgnpCourses()
    {
        return _ognpCourses;
    }

    public IReadOnlyCollection<OgnpFlow> GetOgnpFlowsInOgnpCourse(OgnpCourse ognpCourse)
    {
        if (FindOgnpCourse(ognpCourse) == null)
            return null;
        int ognpCourseIndex = _ognpCourses.IndexOf(ognpCourse);
        return _ognpCourses[ognpCourseIndex].GetOgnpFlows();
    }

    public IReadOnlyCollection<OgnpGroup> GetOgnpGroupsInOgnpFlow(OgnpCourse ognpCourse, OgnpFlow ognpFlow)
    {
        if (FindOgnpCourse(ognpCourse) == null)
            return null;
        return FindOgnpFlow(ognpCourse, ognpFlow) == null
            ? null
            : FindOgnpFlow(ognpCourse, FindOgnpFlow(ognpCourse, ognpFlow)).GetGroupsInOgnpFlow();
    }

    public bool HasGroupAndOgnpGroupSameCouples(Group group, OgnpCourse ognpCourse, OgnpFlow ognpFlow)
    {
        if (_isuService.FindGroup(group.NameOfGroup) == null)
            throw new GroupExistingExeption($"Group with name {group.NameOfGroup.NameOfGroup} doesn't exists");
        if (FindOgnpCourse(ognpCourse) == null)
            throw new OgnpCourseExistingException($"Ognp course with specialization {ognpCourse.Specialization} doesn't exists");
        if (FindOgnpFlow(ognpCourse, ognpFlow) == null)
            throw new OgnpFlowException($"Flow with number {ognpFlow.FlowNumber} doesn't exists in course with specialization {ognpCourse.Specialization}");
        IEnumerable<Couple> intersect = _schedule[group].Schedule.Intersect(ognpFlow.Schedule.Schedule);
        return intersect.Any();
    }

    public bool DoesStudentEnrollOnThisOgnp(Student student, OgnpCourse course)
    {
        if (_isuService.FindStudent(student.Id) == null)
            throw new StudentExistingException($"Student with {student.Id} doesn't exists");
        if (FindOgnpCourse(course) == null)
            throw new OgnpCourseExistingException($"Course with specialization {course.Specialization} doesn't exists");
        IEnumerable<Student> studentOnOgnp = course.GetOgnpFlows()
            .SelectMany(flow => flow.GetGroupsInOgnpFlow().SelectMany(group => group.GetStudents()));
        return studentOnOgnp.Contains(student);
    }

    public int HuwMuchOgnpHasStudent(int studentId)
    {
        if (_isuService.FindStudent(studentId) == null)
            return 0;
        int tempCountOfOgnpCourses = _ognpCourses.SelectMany(course => course.GetOgnpFlows())
            .SelectMany(group => group.GetGroupsInOgnpFlow())
            .SelectMany(student => student.GetStudents())
            .Count(student => student.Id == studentId);

        return tempCountOfOgnpCourses;
    }

    public void CanStudentBeEnrolledOnThisOgnpCourse(Student student, Group group, OgnpCourse ognpCourse, OgnpFlow ognpFlow, OgnpGroup ognpGroup)
    {
        if (_isuService.FindStudent(student.Id) == null)
            throw new StudentExistingException($"Student with {student.Id} doesn't exists");
        int studentsOgnpCourses = HuwMuchOgnpHasStudent(student.Id);
        if (studentsOgnpCourses >= MaxCountOfOgnpCoursesForOneStudent)
            throw new MaxCountOfOgnpException($"Student {student.Id} already has max count of Ognp courses");
        if (_isuService.FindGroup(group.NameOfGroup) == null)
            throw new GroupExistingExeption($"Group with name {group.NameOfGroup.NameOfGroup} doesn't exists");
        if (FindOgnpCourse(ognpCourse) == null)
            throw new OgnpCourseExistingException($"Course with specialization {ognpCourse.Specialization} doesn't exists");
        if (FindOgnpFlow(ognpCourse, ognpFlow) == null)
            throw new FindOgnpFlowException($"Flow with number {ognpFlow.FlowNumber} doesn't exists");
        if (DoesStudentEnrollOnThisOgnp(student, ognpCourse))
            throw new StudentAlreadyEnrollOnOgnpException($"Student with id {student.Id} already enrolled on OGNP {ognpCourse.Specialization}");
        if (HasGroupAndOgnpGroupSameCouples(group, ognpCourse, ognpFlow))
            throw new IntersectTimeTablesException($"Group {group.NameOfGroup.NameOfGroup} has intersect in timetable with flow {ognpFlow.FlowNumber}");
        if (group.NameOfGroup.NameOfGroup[0] == ognpCourse.Specialization)
            throw new SameSpecializationException();
        if (ognpGroup.GetStudents().Count + 1 > ognpGroup.Capacity)
            throw new MaxCountOfStudentsInGroupException($"Group {group.NameOfGroup.NameOfGroup} already has max count of students");
    }

    public Student EnrollStudentOnOgnpCourse(Student student, Group group, OgnpCourse ognpCourse, OgnpFlow ognpFlow, OgnpGroup ognpGroup)
    {
        CanStudentBeEnrolledOnThisOgnpCourse(student, group, ognpCourse, ognpFlow, ognpGroup);
        ognpGroup.AddStudent(student);
        return student;
    }

    public void UnsubscribeStudentFromOgnpCourse(Student student, Group group, OgnpCourse ognpCourse, OgnpFlow ognpFlow, OgnpGroup ognpGroup)
    {
        if (_isuService.FindStudent(student.Id) == null)
            throw new StudentExistingException($"Student with {student.Id} doesn't exists");
        if (_isuService.FindGroup(group.NameOfGroup) == null)
            throw new GroupExistingExeption($"Group with name {group.NameOfGroup.NameOfGroup} doesn't exists");
        if (FindOgnpCourse(ognpCourse) == null)
            throw new FindOgnpCourseException($"Course with specialization {ognpCourse.Specialization} doesn't exists");
        if (FindOgnpFlow(ognpCourse, ognpFlow) == null)
            throw new OgnpFlowException($"Flow with number {ognpFlow.FlowNumber} doesn't exists in course with specialization {ognpCourse.Specialization}");
        foreach (var tempStudent in ognpGroup.GetStudents())
        {
            if (tempStudent.Id == student.Id)
            {
                ognpGroup.DeleteStudent(student);
                return;
            }
        }
    }

    public IReadOnlyCollection<Student> StudentsWithOutOgnpInGroup(Group group)
    {
        if (_isuService.FindGroup(group.NameOfGroup) == null)
            throw new GroupExistingExeption($"Group with {group.NameOfGroup.NameOfGroup} doesn't exists");
        IEnumerable<Student> studentsInGroup = _isuService.FindStudents(group.NameOfGroup);
        var studentsWithOutOgnp = new List<Student>();
        foreach (var tempStudent in studentsInGroup)
        {
            if (HuwMuchOgnpHasStudent(tempStudent.Id) != MaxCountOfOgnpCoursesForOneStudent)
                studentsWithOutOgnp.Add(tempStudent);
        }

        return studentsWithOutOgnp;
    }
}