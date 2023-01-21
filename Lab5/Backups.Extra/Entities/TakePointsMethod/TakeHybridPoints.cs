using Backups.Entities;
using Backups.Extra.Exceptions;
using Newtonsoft.Json;

namespace Backups.Extra.Entities.TakePointsMethod;

public class TakeHybridPoints : ITakePointsMethod
{
    private const int ZeroReturn = 0;
    private const int MinCountPoints = 0;
    [JsonProperty]
    private ITakePointsMethod[] _methods;

    public TakeHybridPoints(ITakePointsMethod[] methods)
    {
        _methods = methods;
    }

    public List<RestorePoint> Execute(List<RestorePoint> points)
    {
        return _methods.Aggregate(points, (current, method) => method.Execute(current));
    }
}