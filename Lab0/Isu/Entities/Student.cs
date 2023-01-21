using System.Reflection;
using Isu.Exceptions;
using Isu.Services;

namespace Isu.Entities;

public class Student
{
    public Student(int id, string firstName, string secondName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new StudentValidationException("Student's first name must not be an empty string or has white spaces");
        }

        if (string.IsNullOrWhiteSpace(secondName))
        {
            throw new StudentValidationException("Student's second name must not be an empty string or has white spaces");
        }

        if (id < 0)
        {
            throw new StudentValidationException("Student's id must be a non-negative integer");
        }

        Id = id;
        FirstName = firstName;
        SecondName = secondName;
    }

    public string FirstName { get; }

    public string SecondName { get; }

    public int Id { get; }
}