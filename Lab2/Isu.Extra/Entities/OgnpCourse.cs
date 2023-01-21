using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class OgnpCourse : IEquatable<OgnpCourse>
{
    private const int MinCountOfFlows = 1;
    private const char MinSpecialization = 'A';
    private const char MaxSpecialization = 'Z';
    private readonly List<OgnpFlow> _ognpFlows;

    public OgnpCourse(char specialization, int countOfFlows)
    {
        EnsureSpecialization(specialization);
        EnsureMaxCountFlowsInOgnp(countOfFlows);
        _ognpFlows = new List<OgnpFlow>();
        Specialization = specialization;
        Capacity = countOfFlows;
    }

    public char Specialization { get; }
    public int Capacity { get; }

    public void EnsureSpecialization(char specialization)
    {
        if (specialization is < MinSpecialization or > MaxSpecialization)
            throw new OgnpSpecializationValidationException($"The Letter of specialization can't be: {specialization}");
    }

    public void EnsureMaxCountFlowsInOgnp(int maxCountOfFlows)
    {
        if (maxCountOfFlows < MinCountOfFlows)
            throw new MaxCountFlowsValidationException($"Max count of flows should be more then {MinCountOfFlows}, you put {maxCountOfFlows}");
    }

    public IReadOnlyCollection<OgnpFlow> GetOgnpFlows()
    {
        return _ognpFlows;
    }

    public bool Equals(OgnpCourse other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Specialization == other.Specialization;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OgnpCourse)obj);
    }

    public override int GetHashCode()
    {
        return Specialization.GetHashCode();
    }

    internal void AddFlow(OgnpFlow newFlow)
    {
        if (newFlow == null)
            throw new ArgumentNullException(nameof(newFlow));
        _ognpFlows.Add(newFlow);
    }
}