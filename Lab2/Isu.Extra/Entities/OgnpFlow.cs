using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class OgnpFlow : IEquatable<OgnpFlow>
{
    private const int MinCountOfGroups = 1;
    private const int MinFlowNumber = 1;
    private readonly List<OgnpGroup> _ognpGroups;

    public OgnpFlow(int flowNumber, int maxCountOfGroups, TableOfCouple schedule)
    {
        EnsureFlowNumber(flowNumber);
        EnsureMaxCountGroupsInOgnpFlow(maxCountOfGroups);
        Schedule = schedule;
        Capacity = maxCountOfGroups;
        FlowNumber = flowNumber;
        _ognpGroups = new List<OgnpGroup>();
    }

    public int FlowNumber { get; }
    public TableOfCouple Schedule { get; }
    public int Capacity { get; }

    public void EnsureMaxCountGroupsInOgnpFlow(int maxCountOfGroups)
    {
        if (maxCountOfGroups <= MinCountOfGroups)
            throw new MaxCountOfGroupsException($"Max count of groups should be more then {MinCountOfGroups}, you put {maxCountOfGroups}");
    }

    public void EnsureFlowNumber(int flowNumber)
    {
        if (flowNumber < MinFlowNumber)
            throw new FlowNumberValidationException($"Flow number should be more then {MinFlowNumber}, you put {flowNumber}");
    }

    public IReadOnlyCollection<OgnpGroup> GetGroupsInOgnpFlow()
    {
        return _ognpGroups;
    }

    public bool Equals(OgnpFlow other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FlowNumber == other.FlowNumber;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OgnpFlow)obj);
    }

    public override int GetHashCode()
    {
        return FlowNumber;
    }

    internal void AddGroup(OgnpGroup newGroup)
    {
        if (newGroup == null)
            throw new ArgumentNullException(nameof(newGroup));
        _ognpGroups.Add(newGroup);
    }
}
