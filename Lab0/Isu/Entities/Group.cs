using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public Group(int groupCapacity, GroupName groupName)
    {
        if (groupCapacity <= 0)
        {
            throw new GroupCapacitySizeException("Invalid size of group capacity");
        }

        GroupCapacity = groupCapacity;
        NameOfGroup = groupName;
    }

    public int GroupCapacity { get; }
    public GroupName NameOfGroup { get; }
}