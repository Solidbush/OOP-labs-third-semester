using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    public GroupName(string groupName)
    {
        EnsureGroupName(groupName);
        Course = new CourseNumber(groupName.Substring(3, 2));
        NameOfGroup = groupName;
    }

    public string NameOfGroup { get; }
    public CourseNumber Course { get; }
    public void EnsureGroupName(string groupName)
    {
        groupName = groupName.Trim();
        if (groupName.Length != 6)
        {
            throw new GroupNameValidationException("The group name must be six characters long");
        }

        if ((groupName[0] < 'A') || (groupName[0] > 'Z'))
        {
            throw new GroupNameValidationException("The first character in group name must be a capital latin letter");
        }

        if (groupName[1] != '3')
        {
            throw new GroupNameValidationException("Incorrect format for undergraduate group name");
        }

        if (groupName[2] < '1' || groupName[2] > '4')
        {
            throw new GroupNameValidationException("Invalid course number");
        }

        if (groupName[5] < '0' || groupName[5] > '9')
        {
            throw new GroupNameSpecializationException();
        }
    }
}