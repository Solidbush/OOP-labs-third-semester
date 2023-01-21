using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    public CourseNumber(string courseNumber)
    {
        if (!int.TryParse(courseNumber, out var numberOfCourse))
        {
            throw new CourseNumberValidationException();
        }

        NumberOfCourse = numberOfCourse;
    }

    public int NumberOfCourse { get; }
}