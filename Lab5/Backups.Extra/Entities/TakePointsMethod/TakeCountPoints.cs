using Backups.Entities;
using Backups.Extra.Exceptions;
using Newtonsoft.Json;

namespace Backups.Extra.Entities.TakePointsMethod;

public class TakeCountPoints : ITakePointsMethod
{
    private const int MinCountPoints = 0;
    [JsonProperty]
    private readonly int _count;
    public TakeCountPoints(int count)
    {
        if (count < MinCountPoints)
            throw new PointsCountException($"Points count should be non-negative number. Your count: {count}");
        _count = count;
    }

    public List<RestorePoint> Execute(List<RestorePoint> points)
    {
        return points.Take(points.Count - _count).ToList();
    }
}